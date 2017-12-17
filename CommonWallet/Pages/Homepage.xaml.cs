using System;
using System.Collections.Generic;
using System.Linq;
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
        public string UserName => data["Username"];

        public static Homepage Instance;
        public Dictionary<string, Wallet> Wallets;

        private readonly List<(Color, Color)> colors = new List<(Color, Color)>();
        private readonly Dictionary<string, string> data;
        private readonly Random rand = new Random();
        public Homepage(Dictionary<string, string> userData)
        {
            InitializeComponent();
            Init();
            Wallets =  new Dictionary<string, Wallet>();
            data = userData;
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
            var data = Server.GetDatabase("UserWallets");
            var wallets = new HashSet<string>();
            for (int i = 0; i < data.First().Value.Count; i++)
            {
                if (data["AccountName"][i] == UserName)
                {
                    wallets.Add(data["WalletGuid"][i]);
                }
            }

            data = Server.GetDatabase("Wallets");


            for (int i = 0; i < data.First().Value.Count; i++)
            {
                if (wallets.Contains(data["Guid"][i]))
                {
                    var walletData = data.Keys.ToDictionary(dataKey => dataKey, dataKey => data[dataKey][i]);
                    var wallet = new Wallet(GetRandomColorPair(), walletData["Wallet"], int.Parse(walletData["Amount"]), walletData["Guid"] );
                    Wallets.Add(wallet.WalletName, wallet);
                }
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

    }
}
