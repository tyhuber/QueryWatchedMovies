using System;
using System.Collections.Generic;
using CsvHelper.TypeConversion;

namespace QueryWatchedMovies.Movies.Converters
{
    public abstract class BaseConverter : DefaultTypeConverter
    {
        public override bool CanConvertFrom(Type type)
        {
            return type == typeof(string);
        }

        public override bool CanConvertTo(Type type)
        {
            return type == typeof(double) || type == typeof(string) || type == typeof(List<string>);
        }

        public override object ConvertFromString(TypeConverterOptions options, string text)
        {
            return ConvertFrom(options, text);
        }

        public override string ConvertToString(TypeConverterOptions options, object value)
        {
            return ConvertTo(options, value);
        }

        protected abstract string ConvertTo(TypeConverterOptions options, object value);
        protected abstract object ConvertFrom(TypeConverterOptions options, string text);

    }
}