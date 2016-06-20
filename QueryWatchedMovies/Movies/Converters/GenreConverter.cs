using System;
using System.Collections.Generic;
using System.Linq;
using CsvHelper.TypeConversion;
using IpaExtensions.IEnumerables;
using IpaExtensions.Objects;

namespace QueryWatchedMovies.Movies.Converters
{
    public class GenreConverter : BaseConverter
    {
        public override bool CanConvertTo(Type type)
        {
            return type == typeof(List<string>) || type == typeof(string);
        }

        protected override string ConvertTo(TypeConverterOptions options, object value)
        {
            if (value == null) return string.Empty;
            return ((List<string>)Convert.ChangeType(value, typeof(List<string>)))?.StringJoin(" ") ?? string.Empty;
        }

        protected override object ConvertFrom(TypeConverterOptions options, string text)
        {
            if (text.IsNullOrWhitespace() || text.EqualIgnoreCase("Unknown")) return new List<string>();
            return text.Split(' ').ToList();
        }
    }
}