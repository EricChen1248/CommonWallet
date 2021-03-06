﻿using System.Drawing;
using System.Windows;
using CommonWallet.Class;
using CommonWallet.UserControls;
using QRCoder;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for WalletPage.xaml
    /// </summary>
    public partial class WalletPage
    {
        public Wallet Wallet;

        public string WalletName
        {
            get => Wallet.WalletName;
            private set => Wallet.WalletName = value;
        }

        public decimal Money
        {
            get => Wallet.Amount;
            private set => Wallet.Amount = value;
        }

        public WalletPage(Wallet wallet)
        {
            Wallet = wallet;
            InitializeComponent();

            WalletLabel.Content = WalletName;
            MoneyLabel.Content = $"{Money:C0}";
            WalletHash.Text = wallet.Guid;

            using (var bitmap = GenerateQrImage(Wallet.Guid))
            {
                QrImage.Source = Helper.BitmapToImageSource(bitmap);
            }
        }

        private static Bitmap GenerateQrImage(string text)
        {
            return new QRCode(new QRCodeGenerator().CreateQrCode(text, QRCodeGenerator.ECCLevel.Q)).GetGraphic(50, "#000000", "#FFFFFF");
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DestroyFloatingFrame();
        }
        

        private void RenameButton(object sender, RoutedEventArgs e)
        {
            using (var renameBox = new Windows.RenameWindow(WalletName))
            {
                renameBox.ShowDialog();
                if (renameBox.DialogResult != true) return;

                WalletName = renameBox.NewName;
                WalletLabel.Content = WalletName;
            }
        }

        private void ImportMoney(object sender, RoutedEventArgs e)
        {

        }

        private void AddUsers(object sender, RoutedEventArgs routedEventArgs)
        {
            MainWindow.Instance.NewFloatingFrame(new AddUsersPage(Wallet, this));   
        }

        private void HistoryBtn_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(new History(Server.GetHistory(Wallet.Guid), this));
        }

        private void SendMoney_Click(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.NewFloatingFrame(new SendMoney(Wallet, this));
        }
    }
}
