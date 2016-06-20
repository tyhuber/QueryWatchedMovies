using System;

namespace QueryWatchedMovies.Extensions
{
    public static class MiscExtensions
    {
        public static bool IsZero(this double d)
        {
            return d < double.Epsilon;
        }
    }
}