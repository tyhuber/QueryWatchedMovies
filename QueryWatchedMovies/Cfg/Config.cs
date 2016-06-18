using System.Xml.Serialization;

namespace QueryWatchedMovies.Cfg
{
    [XmlRoot]
    public class Config
    {
        [XmlAttribute]
        public string CsvPath { get; set; }
    }
}