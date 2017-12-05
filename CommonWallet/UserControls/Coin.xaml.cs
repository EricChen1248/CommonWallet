using System.Windows;
using System.Windows.Media;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for Coin.xaml
    /// </summary>
    public partial class Coin
    {
        public Coin(Color topColor, Color bottomColor)
        {
            InitializeComponent();
            CoinFill.Fill = new LinearGradientBrush(topColor, bottomColor, new Point(0.5,0), new Point(0.5,1));
        }
    }
}
