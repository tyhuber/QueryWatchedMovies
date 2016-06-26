using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using CsvHelper;
using IpaExtensions.FileSystem;
using IpaExtensions.IEnumerables;
using IpaExtensions.Objects;
using QueryWatchedMovies.Cfg;
using QueryWatchedMovies.Movies;

namespace QueryWatchedMovies
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public const string DefaultWatchedPath = @"C:\WatchedMedia\WatchedMovies.csv";

        public static FileInfo WatchedCsvFile => Config?.WatchedCsvFile;
        public static FileInfo DownloadedCsvFile => Config?.DownloadedCsvFile;

        public static Config Config { get; set; }
        private static bool Valid { get; set; }

        public List<Movie> WatchedMovieList { get; set; }
        public List<Movie> DownloadedMovieList { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            if (DeserializeConfig()) return;
            PopulateListBoxes();
        }

        private void PopulateListBoxes()
        {
            WatchedMovieList = WatchedCsvFile.DeserializeCsv<Movie, MovieMap>();
            DownloadedMovieList = DownloadedCsvFile.DeserializeCsv<Movie, MovieMap>();
            if (!WatchedMovieList.Any() && !DownloadedMovieList.Any())
            {
                Error($"No movies found in either {WatchedCsvFile.FullName} or {DownloadedCsvFile.FullName}");
            }
            if (WatchedMovieList.Any())
            {
                WatchedMovies.ItemsSource = WatchedMovieList;
            }
            else
            {
                WatchedMovies.ItemsSource = new[] {$"No movies found in {WatchedCsvFile.FullName}"};
            }
            if (DownloadedMovieList.Any())
            {
                DownloadedMovies.ItemsSource = DownloadedMovieList;
            }
            else
            {
                WatchedMovies.ItemsSource = new[] {$"No movies found in {DownloadedCsvFile.FullName}"};
            }
        }

        private bool DeserializeConfig()
        {
            Valid = true;
            FileInfo configFileInfo = Directory.GetCurrentDirectory().GetFileInfo("Config.xml");
            if (configFileInfo.ExistsNow())
            {
                Config = configFileInfo.DeserializeXml<Config>();
            }
            if (!Config.Valid)
            {
                var invalidSettings = Config.GetInvalidSettings();
                Error(
                    $"Error - Settings are invalid: \n{invalidSettings.StringJoin("\n\t")}. \nUnable to populate watched movies.");
                Application.Current.Shutdown(1);
                return true;
            }
            return false;
        }

        private void Error(string error)
        {
            MessageBox.Show(this, error);
            Valid = false;
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            IEnumerable<Movie> filteredWatchedMovies;
            if (!FilterMovies(WatchedMovieList, searchText, out filteredWatchedMovies)) return;
            WatchedMovies.ItemsSource = filteredWatchedMovies;
            IEnumerable<Movie> filteredDownloadedMovies;
            if (!FilterMovies(DownloadedMovieList, searchText, out filteredDownloadedMovies)) return;
            DownloadedMovies.ItemsSource = filteredDownloadedMovies;
        }

        private static bool FilterMovies(List<Movie> movies, string searchText, out IEnumerable<Movie> filteredMovies)
        {
            filteredMovies=new Movie[0];
            if (movies == null || !movies.Any()) return false;
            filteredMovies = movies.Where(x => x.Title.ToLowerInvariant().Contains(searchText.ToLowerInvariant()));
            return true;
        }

        private void WatchedMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PopulateMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            /*MovieCollection.GetMoviesFromCsv(DefaultWatchedPath);
            MessageBox.Show(this,$"Got {Movies.Count} movies.");*/
        }
    }
}
