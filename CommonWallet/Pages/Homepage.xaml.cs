﻿using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommonWallet.Class;
using CommonWallet.DataClasses;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage
    {
        public string UserName => Account.UserName;
        public readonly AccountData Account;

        public static Homepage Instance;
        public Dictionary<string, Wallet> Wallets;

        private readonly List<(Color, Color)> colors = new List<(Color, Color)>();
        private readonly Random rand = new Random();


        public Homepage(AccountData account)
        {
            InitializeComponent();
            Init();
            Wallets =  new Dictionary<string, Wallet>();

            Account = account;
            AccountNameLabel.Content = Account.AccountName;

            Instance = this;

            GetWallets();
        }

        private void Init()
        {
            colors.Add((0xFF29ffc6.ToColor(), 0xFF0575E6.ToColor()));
            colors.Add((0xFF7303c0.ToColor(), 0xFFec38bc.ToColor()));
            colors.Add((0xFF283c86.ToColor(), 0xFF45a247.ToColor()));
            colors.Add((0xFFc0392b.ToColor(), 0xFF8e44ad.ToColor()));
            colors.Add((0xFF000046.ToColor(), 0xFF1CB5E0.ToColor()));
            colors.Add((0xFF021B79.ToColor(), 0xFF0575E6.ToColor()));

        }

        private (Color, Color) GetRandomColorPair()
        {
            return colors[rand.Next(colors.Count)];
        }

        private void GetWallets()
        {
            var walletsList = Server.GetUserWallets(Account.UserName);
            Wallets.Clear();
            foreach (var data in walletsList)
            {
                var wallet = new Wallet(GetRandomColorPair(), data );
                Wallets.Add(wallet.WalletName, wallet);
            }
            RefreshWallets();
        }

        private void RefreshWallets()
        {
            WalletPanel.ClearChildren();

            foreach (var wallet in Wallets.Values)
            {
                WalletPanel.AddChild(wallet);
            }

            WalletPanel.AllignWallets();
        }
        
        private void AddWalletBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(new NewWallet());
        }

        private void LogoutBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.Logout();
        }

        private void RefreshBtn_Click(object sender, RoutedEventArgs e)
        {
            GetWallets();
            RefreshWallets();
        }
    }
}
