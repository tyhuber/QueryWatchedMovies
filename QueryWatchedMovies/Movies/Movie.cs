using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using IpaExtensions.Objects;
using QueryWatchedMovies.Extensions;

namespace QueryWatchedMovies.Movies
{
    public class Movie
    {
        public string Title { get; set; }
        public string YearString { get; set; }
        public string SizeString { get; set; }
        public double Score { get; set; }
        public string RunTimeString { get; set; }
        public string ImdbRatingString { get; set; }
        public string RtFreshString { get; set; }
        public string RtUserRatingString { get; set; }
        public string MetaScoreString { get; set; }
        public string GenresString { get; set; }

        public int? Year => YearString.ToNullableInt();
        public double Size => SizeString.RegexReplace(new Regex(@"\s*GB\s*")).ToDouble();
        public double RunTime => RunTimeString.ToDouble();
        public double? Imdb => ImdbRatingString.ToNullableDouble();
        public int? RtFresh => RtFreshString.ToNullableInt();
        public int? RtRating => RtUserRatingString.ToNullableInt();
        public int? MetaScore => MetaScoreString.ToNullableInt();
        public List<string> Genres => GenresString.Split(' ').ToList();



        public bool CanParse(string s)
        {
            return s.Trim().EqualIgnoreCase("Unknown");
        }

        #region Overrides of Object

        public override string ToString()
        {
            return $"{Title.Trim()} ({Year?.ToString()??"Unknown"})";
        }

        #endregion
    }
}