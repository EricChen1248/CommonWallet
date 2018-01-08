using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using CommonWallet.Class;
using CommonWallet.DataClasses;


namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for History.xaml
    /// </summary>
    public partial class History
    {
        private readonly WalletPage walletPage;

        public History(IEnumerable<HistoryData> data, WalletPage parentPage)
        {
            InitializeComponent();
            var dataAsList = data as IList<HistoryData> ?? data.ToList();
            foreach (var historyData in dataAsList)
            {
                historyData.Username = Server.GetAccountData(historyData.Username).AccountName;
            }
            dataAsList.Add(new HistoryData{ Username = "總共", Amount = dataAsList.Sum(x => x.Amount), DateTime = DateTime.Now.ToLongDateString()});
            HistoryTable.ItemsSource = dataAsList.Reverse();
            walletPage = parentPage;
        }
        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(walletPage);
        }
    }
}
