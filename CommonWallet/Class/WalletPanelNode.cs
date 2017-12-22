using System.Windows;
using CommonWallet.UserControls;

namespace CommonWallet.Class
{
    class WalletPanelNode
    {
        internal Point Coords;
        internal Wallet Wallet;

        internal WalletPanelNode(Point point, Wallet wallet)
        {
            Coords = point;
            Wallet = wallet;
        }
    }
}
