using System;
using CsvHelper.TypeConversion;
using IpaExtensions.Objects;

namespace QueryWatchedMovies.Movies.Converters
{
    public class DateConverter : BaseConverter
    {
        public override bool CanConvertTo(Type type)
        {
            return type == typeof(DateTime) || type == typeof(string);
        }
        protected override string ConvertTo(TypeConverterOptions options, object value)
        {
            if (value == null) return "Unknown";
            var dt = Convert.ToDateTime(value);
            if (dt == DateTime.MinValue) return "Unknown";
            return dt.ToString("g");
        }

        protected override object ConvertFrom(TypeConverterOptions options, string text)
        {
            DateTime time;
            if (text.EqualIgnoreCase("unknown")) return DateTime.MinValue;
            if (!DateTime.TryParse(text, out time))
            {
                return DateTime.MinValue;
            }
            /*if (!DateTime.TryParseExact(text, "g", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None,
                out time))
            {
                return DateTime.MinValue;
            }*/
            return time;

        }
    }
}