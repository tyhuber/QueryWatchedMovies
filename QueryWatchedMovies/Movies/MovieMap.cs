using CsvHelper.Configuration;

namespace QueryWatchedMovies.Movies
{
    public class MovieMap : CsvClassMap<Movie>
    {
        public MovieMap()
        {
            Map(x => x.Title).Name("Title");
            Map(x => x.YearString).Name("Year");
            Map(x => x.SizeString).Name("Size");
            Map(x => x.Score).Name("Score");
            Map(x => x.RunTimeString).Name("Run Time");
            Map(x => x.ImdbRatingString).Name("IMDB");
            Map(x => x.RtFreshString).Name("RT Fresh");
            Map(x => x.RtUserRatingString).Name("Rt User Rating");
            Map(x => x.MetaScoreString).Name("MetaScore");
            Map(x => x.GenresString).Name("Genres");
        }
    }
}