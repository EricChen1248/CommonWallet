using System;
using System.Windows;
using CommonWallet.Pages;

namespace CommonWallet
{
    /// <summary>
    /// Interaction logic for RenameWindow.xaml
    /// </summary>
    public partial class RenameWindow : Window, IDisposable
    {
        public string NewName { get; private set; }
        private readonly string originalName;
        public RenameWindow(string oName)
        {
            InitializeComponent();
            originalName = oName;
            ONameTextBlock.Text = $"原名 : {originalName}";
        }

        private void ConfirmButton_Click(object sender, RoutedEventArgs e)
        {
            if (RenameBox.Text == "")
            {
                DialogResult = false;
                return;
            }
            if (Homepage.Instance.Wallets.ContainsKey(RenameBox.Text))
            {
                MessageBox.Show("已經有相同名字的錢包了");
                return;
            }

            if (originalName == RenameBox.Text)
            {
                MessageBox.Show("跟原本名字相痛");
                return;
            }

            DialogResult = true;
            NewName = RenameBox.Text;
        }

        public void Dispose()
        {
            
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
