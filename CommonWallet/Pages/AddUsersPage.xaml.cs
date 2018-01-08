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
        private readonly WalletPage parentWallet;
        public AddUsersPage(Wallet parentWallet, WalletPage walletPage)
        {
            InitializeComponent();
            wallet = parentWallet;
            this.parentWallet = walletPage;
            Lister.AddNode(Homepage.Instance.Account, Homepage.Instance.Account.AccountName, false); 

            foreach (var walletUserData in Server.GetUsersOfWallet(wallet.Guid))
            {
                if (walletUserData.UserName == Homepage.Instance.Account.UserName)
                    continue;

                var user = Server.GetAccountData(walletUserData.UserName);
                Lister.AddNode(user, user.AccountName);
            }
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(parentWallet);
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
            var users = Lister.GetDataList<AccountData>().ToList();
            var alreadyIsUser = Server.GetUsersOfWallet(wallet.Guid).Select(u => u.UserName).ToHashSet();

            var newUsers = users.Where(u => !alreadyIsUser.Contains(u.UserName));
            Server.AddWalletUsers(newUsers, wallet.Guid);
            var selectedUsersNames = users.Select(u => u.UserName).ToHashSet();
            var removedUsers = alreadyIsUser.Where(u => !selectedUsersNames.Contains(u));
            Server.RemoveWalletUsers(removedUsers, wallet.Guid);

            MainWindow.Instance.NewFloatingFrame(parentWallet);
        }
    }
}
