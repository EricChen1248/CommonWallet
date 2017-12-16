using System;
using System.Collections.Generic;
using System.Windows.Controls;
using System.Windows.Media;
using CommonWallet.Class;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage
    {
        public static Homepage Instance;
        public Dictionary<string, Wallet> Wallets;

        private List<(Color, Color)> colors = new List<(Color, Color)>();
        private Random rand = new Random();
        public Homepage()
        {
            InitializeComponent();
            Init();
            Wallets =  new Dictionary<string, Wallet>();


            Instance = this;
            // TODO remove test Wallet Control
            var color = GetRandomColorPair();
            var wallet = new Wallet(color.Item1, color.Item2, "主錢包", 100000);
            WalletPanel.AddChild(wallet);
            Wallets.Add(wallet.WalletName, wallet);

            color = GetRandomColorPair();
            wallet = new Wallet(color.Item1, color.Item2, "歐洲旅遊", 2000);
            WalletPanel.AddChild(wallet);
            Wallets.Add(wallet.WalletName, wallet);

            color = GetRandomColorPair();
            wallet = new Wallet(color.Item1, color.Item2, "財金之夜", 25000);
            WalletPanel.AddChild(wallet);
            Wallets.Add(wallet.WalletName, wallet);
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
    }
}
