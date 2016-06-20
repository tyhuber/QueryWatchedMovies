using System;
using CsvHelper.TypeConversion;
using IpaExtensions.Objects;
using QueryWatchedMovies.Extensions;

namespace QueryWatchedMovies.Movies.Converters
{
    public class DoubleConverter : BaseConverter
    {
        protected override string ConvertTo(TypeConverterOptions options, object value)
        {
            double d = Convert.ToDouble(value);
            string format = options.NumberStyle.HasValue ? "#.#" : "###.#";
            string s = d.IsZero() ? "Unknown" : d.ToString(format);
            return s;
        }

        protected override object ConvertFrom(TypeConverterOptions options, string text)
        {
            if (text.IsNullOrWhitespace() || text.EqualIgnoreCase("Unknown"))
            {
                return 0d;
            }
            double d;
            if (!double.TryParse(text, out d))
            {
                return 0;
            }
            return d;
        }
    }
}