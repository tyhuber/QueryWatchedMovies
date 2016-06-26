using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using IpaExtensions.FileSystem;
using IpaExtensions.Objects;

namespace QueryWatchedMovies.Cfg
{
    [XmlRoot]
    public class Config
    {

        [XmlAttribute]
        public string DirectoryPath { get; set; }
        [XmlAttribute]
        public string WatchedFileName { get; set; }
        [XmlAttribute]
        public string DownloadedFileName { get; set; }
        [XmlIgnore]
        private const string DefaultWatchedName = "WatchedMovies.csv";
        [XmlIgnore]
        private const string DefaultDownloadedName = "DownloadedMovies.csv";
        [XmlIgnore]
        private const string DefaultDirPath = @"C:\WatchedMedia";
        [XmlIgnore]
        private DirectoryInfo CsvDirectory => DirectoryPath.IsNullOrWhitespace()?DefaultDirPath.GetDirInfo():DirectoryPath.GetDirInfo();
        [XmlIgnore]
        public FileInfo WatchedCsvFile => GetFile(WatchedFileName, DefaultWatchedName);
        [XmlIgnore]
        public FileInfo DownloadedCsvFile => GetFile(DownloadedFileName, DefaultDownloadedName);
        [XmlIgnore]
        public bool Valid => CsvDirectory.ExistsNow() 
                                && WatchedCsvFile.ExistsNow() 
                                && DownloadedCsvFile.ExistsNow();

        public List<string> GetInvalidSettings()
        {
            List<string> list = new List<string>();
            if(Valid)return list;
            if (!CsvDirectory.ExistsNow())
            {
                list.Add($"Directory containing csv file ({CsvDirectory?.FullName??DirectoryPath}) does not exist.");
                return list;
            }
            if (!WatchedCsvFile.ExistsNow())
            {
                list.Add($"Watched csv file ({WatchedCsvFile?.FullName??"NULL"}) does not exist");
            }
            if (!DownloadedCsvFile.ExistsNow())
            {
                list.Add($"Downloaded csv file ({DownloadedCsvFile?.FullName??"NULL"}) does not exist.");
            }
            return list;

        }

        private FileInfo GetFile(string fileName, string defaultName)
        {
            if (fileName.IsNullOrWhitespace()) return CsvDirectory.GetChildFile(defaultName);
            return CsvDirectory.GetChildFile(fileName);
        }
    }
}