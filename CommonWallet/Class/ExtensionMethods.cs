﻿using System.Collections.Generic;
using System.Windows.Media;

namespace CommonWallet.Class
{
    internal static class ExtensionMethods
    {
        public static Color ToColor(this uint argb)
        {
            return Color.FromArgb((byte)((argb & -16777216) >> 0x18),
                      (byte)((argb & 0xff0000) >> 0x10),
                      (byte)((argb & 0xff00) >> 8),
                      (byte)(argb & 0xff));
        }
        public static HashSet<T> ToHashSet<T>(this IEnumerable<T> source,
            IEqualityComparer<T> comparer = null)
        {
            return new HashSet<T>(source, comparer);
        }
    }
    
}
