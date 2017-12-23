namespace CommonWallet.DataClasses
{
    public class HistoryData
    {
        public int ID { get; set; }
        public string WalletGuid { get; set; }
        public string DateTime { get; set; }
        public string Username { get; set; }
        public decimal Amount { get; set; }
    }
}