using System.Windows;
using System.Windows.Controls;
using CommonWallet.Class.Exceptions;
using CommonWallet.UserControls;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for FundsTransfer.xaml
    /// </summary>
    public partial class FundsTransfer
    {
        private readonly Wallet wallet1;
        private readonly Wallet wallet2;

        private decimal WalletMoney1 => wallet1.Amount;
        private decimal WalletMoney2 => wallet2.Amount;

        private int transfer;
        private bool paymentToggle;

        public FundsTransfer(Wallet wallet1, Wallet wallet2)
        {
            InitializeComponent();
            this.wallet1 = wallet1;
            this.wallet2 = wallet2;

            Namel1Label.Content = wallet1.Name;
            Namel2Label.Content = wallet2.Name;

            Money1Label.Content = $"{WalletMoney1:C0}";
            Money2Label.Content = $"{WalletMoney2:C0}";

            NewMoney1Label.Content = $"{WalletMoney1:C0}";
            NewMoney2Label.Content = $"{WalletMoney2:C0}";
        }

        private void TextBoxBase_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            if (TransferBox.Text == string.Empty)
            {
                transfer = 0;
                UpdateLabels();
                return;
            }
            if (int.TryParse(TransferBox.Text, out var value))
            {
                transfer = value;
                UpdateLabels();
                return;
            }
            TransferBox.Text = transfer.ToString();
        }

        private void UpdateLabels()
        {
            NewMoney1Label.Content = $"{WalletMoney1 + transfer * (paymentToggle ? 1 : -1):C0}";
            NewMoney2Label.Content = $"{WalletMoney2 - transfer * (paymentToggle ? 1 : -1):C0}";

        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            paymentToggle = !paymentToggle;
            ((Button) sender).Content = paymentToggle ? "↑" : "↓" ;
            UpdateLabels();
        }

        private void TransferBtn_OnClick(object sender, RoutedEventArgs e)
        {
            try
            {
                if (WalletMoney1 + transfer * (paymentToggle ? 1 : -1) < 0)
                {
                    throw new NotEnoughMoneyException(wallet1.WalletName);
                }
                if (WalletMoney2 - transfer * (paymentToggle ? 1 : -1) < 0)
                {
                    throw new NotEnoughMoneyException(wallet2.WalletName);
                }
                wallet1.UpdateAmount(WalletMoney1 + transfer * (paymentToggle ? 1 : -1));
                wallet2.UpdateAmount(WalletMoney2 - transfer * (paymentToggle ? 1 : -1));

                MainWindow.Instance.DestroyFloatingFrame();
            }
            catch (NotEnoughMoneyException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            MainWindow.Instance.DestroyFloatingFrame();
        }
    }

}
