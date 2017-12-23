using System.Collections.Generic;
using System.Windows.Media;
using Color = System.Windows.Media.Color;

namespace CommonWallet.UserControls
{
    /// <summary>
    /// Interaction logic for AccountLister.xaml
    /// </summary>
    public partial class Lister
    {
        public Color BorderColor
        {
            set => Border.BorderBrush = new SolidColorBrush(value);
        }

        public Lister()
        {
            InitializeComponent();
        }

        public void AddNode(object nodeData, string nodeDisplay)
        {
            var node = new ListerNode(nodeData, nodeDisplay, this);
            ListerPanel.Children.Add(node);
        }

        internal void RemoveNode(ListerNode node)
        {
            ListerPanel.Children.Remove(node);
        }

        public IEnumerable<T> GetDataList<T>()
        {
            var list = new List<T>();

            foreach (var child in ListerPanel.Children)
            {
                if (child is ListerNode node)
                {
                    list.Add((T) node.Data);
                }
            }

            return list;
        }
    }
}
