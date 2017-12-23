using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommonWallet.Class;

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
            if (account is null)
            {
                AddUserBox.Foreground = new SolidColorBrush(Colors.OrangeRed);
                var timer = new DispatcherTimer {Interval = TimeSpan.FromSeconds(0.25)};
                timer.Tick += (s, args) =>
                {
                    (s as DispatcherTimer)?.Stop();
                    AddUserBox.Foreground = new SolidColorBrush(Colors.Black);
                };
                timer.Start();

                return;
            }

            Lister.AddNode(account, account.AccountName);
            AddUserBox.Text = "";
            
        }
    }
}
