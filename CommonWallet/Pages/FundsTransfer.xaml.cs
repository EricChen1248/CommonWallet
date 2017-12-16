using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using CommonWallet.Class.Exceptions;

namespace CommonWallet.Pages
{
    /// <summary>
    /// Interaction logic for FundsTransfer.xaml
    /// </summary>
    public partial class FundsTransfer
    {
        private string walletName1;
        private string walletName2;
        private int walletMoney1;
        private int walletMoney2;

        private int transfer;
        private bool paymentToggle;

        public FundsTransfer(string name1, int money1, string name2, int money2)
        {
            InitializeComponent();
            walletName1 = name1;
            walletName2 = name2;
            walletMoney1 = money1;
            walletMoney2 = money2;

            Namel1Label.Content = name1;
            Namel2Label.Content = name2;

            Money1Label.Content = $"{walletMoney1:C0}";
            Money2Label.Content = $"{walletMoney2:C0}";

            NewMoney1Label.Content = $"{walletMoney1:C0}";
            NewMoney2Label.Content = $"{walletMoney2:C0}";
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
            NewMoney1Label.Content = $"{walletMoney1 + transfer * (paymentToggle ? 1 : -1):C0}";
            NewMoney2Label.Content = $"{walletMoney2 - transfer * (paymentToggle ? 1 : -1):C0}";

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
                if (walletMoney1 + transfer * (paymentToggle ? 1 : -1) < 0)
                {
                    throw new NotEnoughMoneyException(walletName1);
                }
                if (walletMoney2 - transfer * (paymentToggle ? 1 : -1) < 0)
                {
                    throw new NotEnoughMoneyException(walletName2);
                }
                Homepage.Instance.Wallets[walletName1].UpdateAmount(walletMoney1 + transfer * (paymentToggle ? 1 : -1));
                Homepage.Instance.Wallets[walletName2].UpdateAmount(walletMoney2 - transfer * (paymentToggle ? 1 : -1));

                MainWindow.Instance.DestroyFloatingFrame();
            }
            catch (NotEnoughMoneyException exception)
            {
                MessageBox.Show(exception.Message);
            }
        }
    }

}
