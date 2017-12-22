using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Windows;
using System.Windows.Input;
using CommonWallet.Class;
using CommonWallet.DataClasses;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login
    {
        private readonly Action<AccountData> loginSuccessful;
        public Login(Action<AccountData> login)
        {
            InitializeComponent();
            loginSuccessful = login;

#if DEBUG
            UserBox.Text = "ericchen1248";
            PasswordBox.Password = "password";
#endif

        }

        private void LoginBtn_OnClick(object sender, RoutedEventArgs e)
        {
           TryLogin();
        }

        private void PasswordBox_OnKeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                TryLogin();
            }
        }

        private void FailedLogin()
        { 
            MessageBox.Show(@"Account or Password Error");
        }
        
        private void TryLogin()
        {
            // Hashing Password with SHA1
            var hashedPassword = new ASCIIEncoding().GetString(new SHA1CryptoServiceProvider().ComputeHash(Encoding.ASCII.GetBytes(PasswordBox.Password)));

            var userName = UserBox.Text.ToLower();
            var dataBaseAccount = Server.GetAccountData(userName);

            dataBaseAccount.Password = hashedPassword;
            if (dataBaseAccount.Password != hashedPassword)
            {
                FailedLogin();
                return;
            }

            loginSuccessful(dataBaseAccount);

        }
    }
}
