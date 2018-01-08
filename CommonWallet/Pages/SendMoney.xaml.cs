using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.VisualStyles;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonWallet.Class;
using CommonWallet.DataClasses;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for SendMoney.xaml
    /// </summary>
    public partial class SendMoney : Page
    {
        private WalletData targetWallet;
        private readonly Wallet fromWallet;
        private WalletPage parent;
        private decimal transfer = 0;

        public SendMoney(Wallet wallet, WalletPage parentPage)
        {
            InitializeComponent();
            fromWallet = wallet;
            parent = parentPage;
            WalletMoney.Content = $"{wallet.Amount:C0}";
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TransferBox.Text == string.Empty)
            {
                transfer = 0;
                return;
            }
            if (int.TryParse(TransferBox.Text, out var value))
            {
                transfer = value;
                return;
            }
            TransferBox.Text = transfer.ToString();
        }

        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(parent);
        }

        private void TransferBtn_OnClick(object sender, RoutedEventArgs e)
        {

        }

        private void SearchBtn_Click(object sender, RoutedEventArgs e)
        {
            targetWallet = Server.GetWalletData(WalletBox.Text);
            if (targetWallet == null)
            {
                ResultsLabel.Content = "查詢無錢包";
                return;
            }
            var text = new StringBuilder(targetWallet.Name);
            if (!(Server.GetUsersOfWallet(targetWallet.Guid) is IList<WalletUserData> users)) return;

            foreach (var walletUserData in users)
            {
                text.Append(Server.GetAccountData(walletUserData.UserName).AccountName);
            }
            var userNames = users.Select(x => Server.GetAccountData(x.UserName).AccountName);
        }
    }
}
