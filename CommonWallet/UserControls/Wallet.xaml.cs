using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
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
using System.Windows.Threading;
using CommonWallet.Class;
using CommonWallet.Pages;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for Wallet.xaml
    /// </summary>
    public partial class Wallet
    {
        private readonly Color topColor;
        private readonly Color bottomColor;


        public string WalletName;
        public int Amount {
            get => amount;
            set
            {
                amount = value;
                WalletAmountText.Text = $"{amount:C0}";
            }
        }
        private int amount;
        public readonly string Guid;

        private static Point StartPoint;
        public static bool IsDrag;

        public Wallet((Color, Color) colors, string name, int amount, string guid)
        {
            InitializeComponent();
            Guid = guid;
            
            MouseDown += WalletOnMouseMove;
            MouseMove += WalletOnMouseMove;
            MouseDoubleClick += Wallet_MouseDoubleClick;

            topColor = colors.Item1;
            bottomColor = colors.Item2;

            Amount = amount;
            WalletName = name;

            Border.Stroke = new LinearGradientBrush(topColor, bottomColor, new Point(0.5, 0), new Point(0.5, 1));
            WalletNameText.Text = name;
        }

        private void Wallet_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var walletPage = new WalletPage(this);
            MainWindow.Instance.NewFloatingFrame(walletPage);
        }

        public void DropCoin(Coin coin)
        {
            if (coin.WalletName == WalletName)
            {
                return;
            }
            MainWindow.Instance.NewFloatingFrame(new FundsTransfer(coin.WalletName, coin.WalleyMoney, WalletName, Amount));
        }

        private void WalletOnMouseMove(object sender, MouseEventArgs mouseEventArgs)
        {
            var mousePos = mouseEventArgs.GetPosition(null);
            var diff = StartPoint - mousePos;

            if (IsDrag || mouseEventArgs.LeftButton != MouseButtonState.Pressed ||
                !(Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance) &&
                !(Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance)) return;

            IsDrag = true;

            if (sender is Wallet == false)
            {
                var temp = Helper.GetParent<Wallet>((Ellipse)sender) as Wallet;
                GenerateCoin(temp, mouseEventArgs.GetPosition(Homepage.Instance.Grid));
            }
            else
            {
                GenerateCoin((Wallet) sender, mouseEventArgs.GetPosition(Homepage.Instance.Grid));
            }
        }

        private static void WalletOnMouseMove(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            StartPoint = mouseButtonEventArgs.GetPosition(null);
        }

        private void GenerateCoin(Wallet sender, Point mousePosition)
        {
            var coin = new Coin(topColor, bottomColor, sender.WalletName, sender.Amount);
            Homepage.Instance.Grid.Children.Add(coin);
            coin.VerticalAlignment = VerticalAlignment.Top;
            coin.HorizontalAlignment = HorizontalAlignment.Left;
            coin.Margin = new Thickness(mousePosition.X - coin.Width / 2, mousePosition.Y - coin.Height / 2, 0, 0);
        }

        public void UpdateAmount(int newAmount)
        {
            Amount = newAmount;
        }

    }
}
