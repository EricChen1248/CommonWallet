using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using CommonWallet.Class;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for WalletPanel.xaml
    /// </summary>
    public partial class WalletPanel
    {
        private const int TokenWidth = 250;
        private const int TokenHeight = 280;
        internal LinkedList<WalletPanelNode> Wallets = new LinkedList<WalletPanelNode>();
        public WalletPanel()
        {
            InitializeComponent();
        }

        public void AddChild(Wallet wallet)
        {
            var count = Wallets.Count;
            var walletPerLine = (int) Panel.Width / TokenWidth;
            var x = (count % walletPerLine) * TokenWidth + 50;
            var y = count / walletPerLine * TokenHeight + count % 2 == 0 ? 50 : 100;

            var node = new WalletPanelNode(new Point(x, y), wallet);

            Wallets.AddLast(node);
            Panel.Children.Add(wallet);

            DisplayWallets();
        }

        public void DisplayWallets()
        {
            foreach (var walletPanelNode in Wallets)
            {
                var wallet = walletPanelNode.Wallet;
                var point = walletPanelNode.Coords;
                wallet.Margin = new Thickness((int) point.X, (int) point.Y, 0, 0);
            }
        }
    }
}
