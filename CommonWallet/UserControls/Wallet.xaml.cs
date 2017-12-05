using System;
using System.Collections.Generic;
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

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for Wallet.xaml
    /// </summary>
    public partial class Wallet
    {
        public Wallet(Color topColor, Color bottomColor)
        {
            InitializeComponent();
            Border.Stroke = new LinearGradientBrush(topColor, bottomColor, new Point(0.5, 0), new Point(0.5, 1));


        }
        
    }
}
