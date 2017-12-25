using System.Windows;
using CommonWallet.Class;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for ListerNode.xaml
    /// </summary>
    public partial class ListerNode
    {
        public readonly object Data;
        private readonly Lister parent;
        public ListerNode(object data, string name, Lister parentLister, bool removable = true)
        {
            InitializeComponent();
            Data = data;
            parent = parentLister;
            NameBlock.Text = name;
            Width = NameBlock.RequiredSize().width + 38;
            Height = 30;

            if (removable) return;
            CloseBtn.Visibility = Visibility.Hidden;
            Width -= CloseBtn.Width;
            var nBMargin = NameBlock.Margin;
            NameBlock.Margin = new Thickness(nBMargin.Left, nBMargin.Top, nBMargin.Right - CloseBtn.Width, nBMargin.Bottom );
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            parent.RemoveNode(this);
        }
    }
}
