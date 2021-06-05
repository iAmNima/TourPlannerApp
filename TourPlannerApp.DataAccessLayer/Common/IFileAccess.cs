using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.Common
{
    public interface IFileAccess
    {
        int CreateNewTourEntryFile(string name, string description, double distance);
        int CreateNewLogEntryFile(TourEntry log_tourEntry,
            DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating);
        IEnumerable<FileInfo> SearchFiles(string searchTerm, EntryTypes searchType);
        IEnumerable<FileInfo> GetAllFiles(EntryTypes searchType);
    }
}
