using System;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
        public static BitmapImage BitmapToImageSource(Bitmap bitmap)
        {
            using (var memory = new MemoryStream())
            {
                bitmap.Save(memory, System.Drawing.Imaging.ImageFormat.Bmp);
                memory.Position = 0;
                var bitmapimage = new BitmapImage();
                bitmapimage.BeginInit();
                bitmapimage.StreamSource = memory;
                bitmapimage.CacheOption = BitmapCacheOption.OnLoad;
                bitmapimage.EndInit();

                return bitmapimage;
            }
        }

    }
}
