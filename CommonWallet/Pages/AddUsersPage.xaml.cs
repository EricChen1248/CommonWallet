using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonWallet.Class;
using CommonWallet.DataClasses;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for AddUsersPage.xaml
    /// </summary>
    public partial class AddUsersPage
    {
        private readonly Wallet wallet;
        public AddUsersPage(Wallet parentWallet)
        {
            InitializeComponent();
            wallet = parentWallet;
            Lister.AddNode(Homepage.Instance.Account, Homepage.Instance.Account.AccountName, false); 

            foreach (var walletUserData in Server.GetUsersOfWallet(wallet.Guid))
            {
                if (walletUserData.UserName == Homepage.Instance.Account.UserName)
                    return;

                var user = Server.GetAccountData(walletUserData.UserName);
                Lister.AddNode(user, user.AccountName);
            }
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DestroyFloatingFrame();
        }

        private void AddUserBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key != Key.Enter)
            {
                return;
            }
            var account = Server.GetAccountData(AddUserBox.Text);
            if (account is null || Lister.GetDataList<AccountData>().Contains(account))
            {
                if (!(FindResource("BlinkNoUser") is Storyboard blink)) return;
                Storyboard.SetTarget(blink, AddUserBox);
                blink.Begin();
                return;
            }

            Lister.AddNode(account, account.AccountName);
            AddUserBox.Text = "";
        }

        private void CompleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var users = Lister.GetDataList<AccountData>();
            var alreadyIsUser = Server.GetUsersOfWallet(wallet.Guid).Select(u => u.UserName);

            var newUsers = users.Where(u => !alreadyIsUser.Contains(u.AccountName));
            foreach (var accountData in newUsers)
            {
                
            }
        }
    }
}
