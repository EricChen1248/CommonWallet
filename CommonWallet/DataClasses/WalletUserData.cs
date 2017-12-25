using LiteDB;

namespace CommonWallet.DataClasses
{
    public class WalletUserData
    {
        public int ID { get; set; }
        [BsonField("AccountName")]
        public string UserName { get; set; }
        public string WalletGuid { get; set; }
    }
}
