using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using CommonWallet.Class.Exceptions;
using CommonWallet.DataClasses;
using LiteDB;

namespace CommonWallet.Class
{
    // Reference: http://www.litedb.org/

    /// <summary>
    /// Handles file IO for our database.
    /// </summary>
    internal static class Server
    {
        private const string ServerLocation = "Resources/Database/Database.db";

        /// <summary>
        /// Pulls the data from file in a Dictionary of Lists with headers as keys
        /// </summary>
        /// <param name="dataBaseName"></param>
        /// <returns></returns>
        [Obsolete]
        public static Dictionary<string, List<string>> GetDatabase(string dataBaseName)
        {
            var results = new Dictionary<string, List<string>>();
            using( var sr = new StreamReader(ServerLocation + dataBaseName + ".txt"))
            {
                // Read in headers for database
                var firstLine = sr.ReadLine()?.Split(',');
                var headers = new List<string>();

                if (firstLine == null)
                    throw new DatabaseError();
                foreach (var s in firstLine)
                {
                    if (s == "")
                        continue;

                    results[s] = new List<string>();
                    headers.Add(s);
                }

                // Read in the rest of database until end
                while (sr.EndOfStream == false)
                {
                    var line = sr.ReadLine()?.Split(',');

                    for (var i = 0; i < headers.Count; i++)
                    {
                        if (line == null)
                            throw new DatabaseError();

                        results[headers[i]].Add(line[i]);
                    }
                }
            }

            return results;
        }

        public static IEnumerable<HistoryData> GetHistory(string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<HistoryData>("TransactionHistory").Find(Query.EQ("WalletGuid", walletGuid)).ToList();
            }
        }

        public static IEnumerable<WalletData> GetUserWallets(string userName)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var walletsGuid = db.GetCollection<WalletUserData>("UserWallets").Find(x => x.AccountName == userName).Select(x => x.WalletGuid).ToHashSet();
                return db.GetCollection<WalletData>("Wallets").Find(x => walletsGuid.Contains(x.Guid)).Select(x => x).ToList();
            }
        }

        public static WalletData GetWalletData(string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<WalletData>("Wallets").Find(x => x.Guid == walletGuid).FirstOrDefault();
            }
        }

        public static AccountData GetAccountData(string userName)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<AccountData>("Accounts").Find(x => x.UserName == userName).FirstOrDefault();
            }   
        }
        
        public static void AddWallet(string accountName, WalletData walletData)
        {
            var userWallet = new WalletUserData
            {
                AccountName = accountName,
                WalletGuid = walletData.Guid
            };

            using (var db = new LiteDatabase(ServerLocation))
            {
                var userWallets = db.GetCollection<WalletUserData>("UserWallets");
                userWallets.Insert(userWallet);

                var wallets = db.GetCollection<WalletData>("Wallets");
                wallets.Insert(walletData);
            }
        }

        public static void UpdateData<T>(T account, string tableName, string indexOn)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var accountsDb = db.GetCollection<T>(tableName);
                accountsDb.Update(account);

                if (indexOn != null)
                {
                    accountsDb.EnsureIndex(indexOn);
                }
            }
        }

        public static void WriteTransaction(HistoryData historyData)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var history = db.GetCollection<HistoryData>("TransactionHistory");
                history.Insert(historyData);
                history.EnsureIndex("WalletGuid");

            }
        }
    }
}