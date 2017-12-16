using System;
using System.Windows;
using System.Windows.Media;
using CommonWallet.UserControls;

namespace CommonWallet.Class
{
    internal static class Helper
    {
        public static object GetParent<T>(DependencyObject sender)
        {
            while (true)
            {
                var result = VisualTreeHelper.GetParent(sender ?? throw new ArgumentNullException(nameof(sender)));
                if (result is T) return result ;
                sender = result;
            }
        }
    }
}
