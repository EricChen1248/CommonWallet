using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using CommonWallet.Class;
using CommonWallet.DataClasses;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for NewWallet.xaml
    /// </summary>
    public partial class NewWallet
    {
        public NewWallet()
        {
            InitializeComponent();
            Lister.BorderColor = Colors.LightGray;
            Lister.AddNode(Homepage.Instance.Account, Homepage.Instance.Account.AccountName, false);
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
    }
}
