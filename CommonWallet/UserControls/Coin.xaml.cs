using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using CommonWallet.Pages;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for Coin.xaml
    /// </summary>
    public partial class Coin
    {
        public static Coin Instance;
        public Wallet Wallet;
        private static DispatcherTimer Timer;

        public Coin(Color topColor, Color bottomColor, Wallet wallet)
        {
            InitializeComponent();
            Wallet = wallet;
            CoinFill.Fill = new LinearGradientBrush(topColor, bottomColor, new Point(0.5,0), new Point(0.5,1));
            
            CoinFill.MouseLeftButtonUp += CoinFill_MouseLeftButtonUp;
            Timer = new DispatcherTimer {Interval = TimeSpan.FromMilliseconds(5)};
            Timer.Tick += Timer_Tick;
            Timer.Start();
            Instance = this;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            var position = Mouse.GetPosition(Homepage.Instance.Grid);
            Instance.Margin = new Thickness(position.X - Width / 2, position.Y - Height / 2, 0, 0);
        }

        private static void CoinFill_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            Homepage.Instance.Grid.Children.Remove(Instance);
            Wallet.IsDrag = false;

            var wallet = (from walletPanelWallet in Homepage.Instance.WalletPanel.Wallets
                          let results = VisualTreeHelper.HitTest(walletPanelWallet.Wallet.Border, 
                                                                 e.GetPosition(walletPanelWallet.Wallet))
                          where !(results is null)
                          select walletPanelWallet.Wallet).FirstOrDefault();

            wallet?.DropCoin(Instance);
            Timer.Stop();
            Timer = null;
            Instance = null;
        }
        
    }
}
