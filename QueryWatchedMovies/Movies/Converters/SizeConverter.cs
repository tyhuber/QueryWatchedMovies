using System;
using CsvHelper.TypeConversion;
using IpaExtensions.Objects;

namespace QueryWatchedMovies.Movies.Converters
{
    public class SizeConverter : BaseConverter
    {
        protected override string ConvertTo(TypeConverterOptions options, object value)
        {
            if (value == null) return "0 GB";
            //            Log("Converting size to string.");
            double d = Convert.ToDouble(value);
            string s = d.ToString("##.# GB");
            //            Log($"Got double {d}. Converted it to {s}");
            return s;
        }

        protected override object ConvertFrom(TypeConverterOptions options, string text)
        {
            if (text.IsNullOrWhitespace())
            {
                return 0d;
            }
            //            Log($"Converting size from string. String = {text}");
            text = text.Replace("GB", string.Empty).Trim();
            //            Log($"After replacing GB - {text}");
            double d;
            if (!double.TryParse(text, out d))
            {
                return 0d;
            }
            return d;
        }
    }
}