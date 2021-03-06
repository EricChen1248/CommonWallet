﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Principal;
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
        private static readonly Dictionary<Type, string> TableNames = new Dictionary<Type, string>();

        public static void InitalizeServer()
        {
            TableNames.Add(typeof(WalletData), "Wallets");
            TableNames.Add(typeof(WalletUserData), "UserWallets");
            TableNames.Add(typeof(HistoryData), "TransactionHistory");
            TableNames.Add(typeof(AccountData), "Accounts");

        }
        public static IEnumerable<HistoryData> GetHistory(string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<HistoryData>(TableNames[typeof(HistoryData)]).Find(Query.EQ("WalletGuid", walletGuid)).ToList();
            }
        }

        public static IEnumerable<WalletData> GetUserWallets(string userName)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var walletsGuid = db.GetCollection<WalletUserData>(TableNames[typeof(WalletUserData)]).Find(x => x.UserName == userName).Select(x => x.WalletGuid).ToHashSet();
                return db.GetCollection<WalletData>(TableNames[typeof(WalletData)]).Find(x => walletsGuid.Contains(x.Guid)).ToList();
            }
        }

        public static IEnumerable<WalletUserData> GetUsersOfWallet(string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<WalletUserData>(TableNames[typeof(WalletUserData)]).Find(x => x.WalletGuid == walletGuid).ToList();
            }
        }
        public static WalletData GetWalletData(string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<WalletData>(TableNames[typeof(WalletData)]).Find(x => x.Guid == walletGuid).FirstOrDefault();
            }
        }

        public static AccountData GetAccountData(string userName)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                return db.GetCollection<AccountData>(TableNames[typeof(AccountData)]).Find(x => x.UserName == userName).FirstOrDefault();
            }   
        }
        
        public static void AddWallet(string accountName, WalletData walletData)
        {
            var userWallet = new WalletUserData
            {
                UserName = accountName,
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

        public static void UpdateData<T>(T data, string tableName, string indexOn)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var table = db.GetCollection<T>(tableName);
                table.Update(data);

                if (indexOn != null)
                {
                    table.EnsureIndex(indexOn);
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

        public static void AddWalletUsers(IEnumerable<AccountData> accounts, string walletGuid)
        {
            var accountDatas = accounts.ToList();

            using (var db = new LiteDatabase(ServerLocation))
            {
                var data =
                    from account in accountDatas
                    select new WalletUserData
                    {
                        UserName = account.UserName,
                        WalletGuid = walletGuid
                    };

                var table = db.GetCollection<WalletUserData>("UserWallets");
                table.InsertBulk(data);

                table.EnsureIndex(x => x.UserName);
                table.EnsureIndex(x => x.WalletGuid);
            }
        }

        public static void RemoveWalletUsers(IEnumerable<string> userNames, string walletGuid)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var hash = userNames.ToHashSet();
                var table = db.GetCollection<WalletUserData>(TableNames[typeof(WalletUserData)]);
                table.Delete(x => hash.Contains(x.UserName));

            }
        }

        public static void AddUser(AccountData account)
        {
            using (var db = new LiteDatabase(ServerLocation))
            {
                var table = db.GetCollection<AccountData>("Accounts");
                table.Insert(account);
                table.EnsureIndex(x => x.UserName);
            }
        }
    }
}