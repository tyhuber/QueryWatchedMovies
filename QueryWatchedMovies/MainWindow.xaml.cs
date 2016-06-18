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
        private const string DefaultCsvPath = @"C:\WatchedMedia\WatchedMovies.csv";

        private static string CsvPath { get; set; }

        private static Config Config { get; set; }
        private static bool Valid { get; set; }

        public List<Movie> Movies { get; set; }

        public MainWindow()
        {
            InitializeComponent();
            Valid = true;
            FileInfo configFileInfo = Directory.GetCurrentDirectory().GetFileInfo("Config.xml");
            CsvPath = DefaultCsvPath;
            if (configFileInfo.ExistsNow())
            {
                Config = configFileInfo.DeserializeXml<Config>();
                if (Config != default(Config))
                {
                    CsvPath = Config.CsvPath;
                }
            }
            if (!CsvPath.Exists())
            {
                MessageBox.Show(this, $"Error: Csv path {CsvPath} does not exist. Unable to populate watched movies.");
                Valid = false;
                Application.Current.Shutdown(1);
                return;
            }
            PopulateWatchedMovies();
        }

        private void textBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            string searchText = SearchBox.Text;
            if(Movies==null||!Movies.Any())return;
            var filteredMovies = Movies.Where(x => x.Title.ToLowerInvariant().Contains(searchText.ToLowerInvariant()));
            WatchedMovies.ItemsSource = filteredMovies;
        }

        private void WatchedMovies_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void PopulateMoviesButton_Click(object sender, RoutedEventArgs e)
        {
            PopulateWatchedMovies();
            MessageBox.Show(this,$"Got {Movies.Count} movies.");
        }

        private void PopulateWatchedMovies()
        {
            using (var csv = new CsvReader(new StreamReader(DefaultCsvPath)))
            {
                csv.Configuration.RegisterClassMap<MovieMap>();
                Movies = csv.GetRecords<Movie>().ToList();
            }
            WatchedMovies.ItemsSource = Movies;
        }
    }
}
