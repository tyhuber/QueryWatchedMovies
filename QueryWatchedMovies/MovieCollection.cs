using System.Collections.Generic;
using System.IO;
using System.Linq;
using CsvHelper;
using QueryWatchedMovies.Movies;

namespace QueryWatchedMovies
{
    public static class MovieCollection
    {
        public static IEnumerable<Movie> GetMoviesFromCsv(FileInfo csvFile)
        {
            using (var csv = new CsvReader(new StreamReader(csvFile.FullName)))
            {
                csv.Configuration.RegisterClassMap<MovieMap>();
                return csv.GetRecords<Movie>().ToList();
            }
        }
    }
}