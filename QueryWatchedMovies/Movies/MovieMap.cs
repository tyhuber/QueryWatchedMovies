using System.Globalization;
using CsvHelper.Configuration;
using QueryWatchedMovies.Movies.Converters;

namespace QueryWatchedMovies.Movies
{
    public class MovieMap : CsvClassMap<Movie>
    {
        public MovieMap()
        {
            Map(x => x.Title).Name("Title");
            Map(x => x.Year).Name("Year");
            Map(x => x.Size).TypeConverter<SizeConverter>().Name("Size");
            Map(x => x.Score).TypeConverter<DoubleConverter>().Name("Score");
            Map(x => x.RunTime).Name("Run Time");
            Map(x => x.Imdb).TypeConverter<DoubleConverter>().TypeConverterOption(NumberStyles.AllowTrailingSign).Name("IMDB");
            Map(x => x.RtFresh).TypeConverter<DoubleConverter>().Name("RT Fresh");
            Map(x => x.RtRating).TypeConverter<DoubleConverter>().Name("Rt User Rating");
            Map(x => x.MetaCritic).TypeConverter<DoubleConverter>().Name("MetaScore");
            Map(x => x.Genres).TypeConverter<GenreConverter>().Name("Genres");
            Map(x => x.DateModified).TypeConverter<DateConverter>().Name("Date Modified");
            Map(x => x.PresentLocally).Name("Present Locally");
        }
    }
}