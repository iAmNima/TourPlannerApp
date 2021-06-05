using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TourPlannerApp.DataAccessLayer.Common;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.FileServer
{
    public class FileAccess : IFileAccess
    {
        private string filePath;

        public FileAccess(string filePath)
        {
            this.filePath = filePath;
        }

        private IEnumerable<FileInfo> GetFileInfos(string startFolder, EntryTypes searchType)
        {
            DirectoryInfo dir = new DirectoryInfo(startFolder);
            return dir.GetFiles("*" + searchType.ToString() + ".txt", SearchOption.AllDirectories);
        }

        private string GetFullPath(string fileName)
        {
            return Path.Combine(filePath, Path.GetFileName(fileName));
        }

        public IEnumerable<FileInfo> SearchFiles(string searchTerm, EntryTypes searchType)
        {

            IEnumerable<FileInfo> fileList = GetFileInfos(filePath, searchType);

            // Search the contents of each file.    
            IEnumerable<FileInfo> queryMatchingFiles =
                from file in fileList
                let fileText = GetFileText(file)
                where fileText.Contains(searchTerm)
                select file;

            return queryMatchingFiles;
        }

        private string GetFileText(FileInfo file)
        {
            using (StreamReader sr = file.OpenText())
            {
                StringBuilder sb = new StringBuilder();
                while (!sr.EndOfStream)
                {
                    sb.Append(sr);
                }
                return sb.ToString();
            }
        }

        public int CreateNewLogEntryFile(TourEntry log_tourEntry, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating)
        {
            // Generate new Id for new file
            int id = Guid.NewGuid().GetHashCode();

            // Create a file to write to
            string fileName = id + "_LogEntry.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(id);
                sw.WriteLine(logDate);
                sw.WriteLine(report);
                sw.WriteLine(distance);
                sw.WriteLine(totalTime);
                sw.WriteLine(rating);
                sw.WriteLine(log_tourEntry.Id);
            }
            return id;
        }

        public int CreateNewTourEntryFile(string name, string description, double distance)
        {
            // Generate new Id for new file
            int id = Guid.NewGuid().GetHashCode();

            // Create a file to write to
            string fileName = id + "_tourEntry.txt";
            string path = GetFullPath(fileName);
            using (StreamWriter sw = File.CreateText(path))
            {
                sw.WriteLine(id);
                sw.WriteLine(name);
                sw.WriteLine(description);
                sw.WriteLine(distance);
            }
            return id;
        }

        public IEnumerable<FileInfo> GetAllFiles(EntryTypes searchType)
        {
            return GetFileInfos(filePath, searchType);
        }

    }
}
