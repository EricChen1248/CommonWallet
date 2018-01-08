using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CommonWallet.Class;
using CommonWallet.DataClasses;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for CreateNewUser.xaml
    /// </summary>
    public partial class NewUser : Page
    {
        public NewUser()
        {
            InitializeComponent();
        }


        private void ReturnBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DestroyFloatingFrame();
        }

        private void CreateNewUser(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                SignupBtn_OnClick(SignupBtn, e);
            }
        }

        private void SignupBtn_OnClick(object sender, RoutedEventArgs e)
        {
            if (PasswordBox.Password != PasswordBoxCopy.Password)
            {
                MessageBox.Show("密碼不符合");
                return;
            }
            var account = Server.GetAccountData(UsernameBox.Text);
            if (account != null)
            {
                MessageBox.Show("已經有此使用者");
                return;
            }

            Server.AddUser(new AccountData
            {
                UserName = UsernameBox.Text,
                AccountName = AccountNameBox.Text,
                Password = new ASCIIEncoding().GetString(
                    new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(PasswordBox.Password)))
            });

            if (MainWindow.Instance.Frame.Content is Login login)
            {
                login.UserBox.Text = UsernameBox.Text;
                login.PasswordBox.Password = "";
            }
            MainWindow.Instance.DestroyFloatingFrame();
        }
    }
}
