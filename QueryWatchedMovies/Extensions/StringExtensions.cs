using System;
using System.Text.RegularExpressions;
using IpaExtensions.Objects;

namespace QueryWatchedMovies.Extensions
{
    public static class StringExtensions
    {
        public static string RegexReplace(this string s, Regex regex)
        {
            if (!regex.IsMatch(s)) return s;
            return regex.Replace(s,String.Empty);
        }
        public static int? ToNullableInt(this string s)
        {
            if (s.IsUnknown()) return null;
            int i;
            if (s.TryToInt(out i)) return i;
            return null;
        }

        public static double? ToNullableDouble(this string s)
        {
            if (s.IsUnknown()) return null;
            double d;
            if (s.TryToDouble(out d)) return d;
            return null;
        }

        private static bool IsUnknown(this string s)
        {
            return s.EqualIgnoreCase("Unknown");
        }
        public static int ToInt(this string s)
        {
            int i;
            if (s.TryToInt(out i)) return i;
            return -1;
        }

        public static double ToDouble(this string s)
        {
            double d;
            if (s.TryToDouble(out d)) return d;
            return -1;
        }

        public static bool TryToInt(this string s, out int i)
        {
            return int.TryParse(s, out i);
        }

        public static bool TryToDouble(this string s, out double d)
        {
            return double.TryParse(s, out d);
        }
    }
}