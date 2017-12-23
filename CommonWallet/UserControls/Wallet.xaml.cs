using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using CommonWallet.Class;
using CommonWallet.DataClasses;
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


        public string WalletName
        {
            get => walletData.Name;
            set
            {
                walletData.Name = value;
                WalletNameText.Text = value;
            }
        }

        public decimal Amount
        {
            get => walletData.Money;
            set
            {
                walletData.Money = value;
                WalletAmountText.Text = $"{(int)value:C0}";
            }
        }
        public string Guid => walletData.Guid;

        private readonly WalletData walletData;
        private static Point StartPoint;
        public static bool IsDrag;

        public Wallet((Color, Color) colors, WalletData walletData)
        {
            InitializeComponent();
            this.walletData = walletData;
            WalletAmountText.Text = $"{(int)Amount:C0}";
            WalletNameText.Text = WalletName;

            MouseDown += WalletOnMouseMove;
            MouseMove += WalletOnMouseMove;
            MouseDoubleClick += Wallet_MouseDoubleClick;

            topColor = colors.Item1;
            bottomColor = colors.Item2;


            Border.Stroke = new LinearGradientBrush(topColor, bottomColor, new Point(0.5, 0), new Point(0.5, 1));
        }

        private void Wallet_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            var walletPage = new WalletPage(this);
            MainWindow.Instance.NewFloatingFrame(walletPage);
        }

        public void DropCoin(Coin coin)
        {
            if (Equals(coin.Wallet, this))
            {
                return;
            }
            MainWindow.Instance.NewFloatingFrame(new FundsTransfer(coin.Wallet, this));
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
                GenerateCoin((Wallet)sender, mouseEventArgs.GetPosition(Homepage.Instance.Grid));
            }
        }

        private static void WalletOnMouseMove(object sender, MouseButtonEventArgs mouseButtonEventArgs)
        {
            StartPoint = mouseButtonEventArgs.GetPosition(null);
        }

        private void GenerateCoin(Wallet sender, Point mousePosition)
        {
            var coin = new Coin(topColor, bottomColor, this);
            Homepage.Instance.Grid.Children.Add(coin);
            coin.VerticalAlignment = VerticalAlignment.Top;
            coin.HorizontalAlignment = HorizontalAlignment.Left;
            coin.Margin = new Thickness(mousePosition.X - coin.Width / 2, mousePosition.Y - coin.Height / 2, 0, 0);
        }

        public void UpdateAmount(decimal newAmount)
        {
            Amount = newAmount;
        }

    }
}
