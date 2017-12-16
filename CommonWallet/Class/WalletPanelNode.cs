using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
