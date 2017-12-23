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
        public ListerNode(object data, string name, Lister parentLister)
        {
            InitializeComponent();
            Data = data;
            parent = parentLister;
            NameBlock.Text = name;
            Width = NameBlock.RequiredSize().width + 38;
            Height = 30;
        }

        private void CloseBtn_OnClick(object sender, RoutedEventArgs e)
        {
            parent.RemoveNode(this);
        }
    }
}
