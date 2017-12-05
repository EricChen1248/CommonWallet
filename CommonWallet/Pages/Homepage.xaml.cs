using System.Windows.Controls;
using CommonWallet.Class;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for Homepage.xaml
    /// </summary>
    public partial class Homepage : Page
    {
        public Homepage()
        {
            InitializeComponent();

            // TODO remove test Wallet Control
            var wallet = new Wallet(0xFF29ffc6.ToColor(), 0xFF0575E6.ToColor());
            WalletPanel.Children.Add(wallet);
        }
    }
}
