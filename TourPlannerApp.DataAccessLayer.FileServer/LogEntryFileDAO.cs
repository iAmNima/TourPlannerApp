using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TourPlannerApp.DataAccessLayer.Common;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.FileServer
{
    public class LogEntryFileDAO : ILogEntryDAO
    {
        private IFileAccess fileAccess;
        private ITourEntryDAO tourEntryDAO;

        public LogEntryFileDAO()
        {
            this.fileAccess = DALFactory.GetFileAccess();
            this.tourEntryDAO = DALFactory.CreateTourEntryDAO();
        }

        public LogEntry AddNewLog(TourEntry log_tourEntry, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating)
        {
            int id = fileAccess.CreateNewLogEntryFile(log_tourEntry, logDate, report, distance, totalTime, rating);
            return new LogEntry(id, log_tourEntry, logDate, report, distance, totalTime, rating);
        }

        public void DeleteLog(LogEntry log)
        {
            throw new NotImplementedException();
        }


        public void DeleteLogsByTour(TourEntry tour)
        {
            throw new NotImplementedException();
        }

        public LogEntry FindById(int id)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(id.ToString(), EntryTypes.LogEntry);
            return QueryFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<LogEntry> GetLogsByTour(TourEntry tour)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(tour.Id.ToString(), EntryTypes.LogEntry);
            return QueryFromFileSystem(foundFiles);
        }

        public void UpdateLog(LogEntry log, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<LogEntry> QueryFromFileSystem(IEnumerable<FileInfo> foundFiles)
        {
            List<LogEntry> foundLogs = new List<LogEntry>();

            foreach (FileInfo file in foundFiles)
            {
                string[] fileLines = File.ReadAllLines(file.FullName);
                foundLogs.Add(new LogEntry(
                    int.Parse(fileLines[0]),                                // id
                    tourEntryDAO.FindById(int.Parse(fileLines[1])),         // mediaItemId
                    DateTime.Parse(fileLines[2]),                           // logDate
                    fileLines[3],                                           // report
                    Double.Parse(fileLines[4]),                             // distance
                    TimeSpan.Parse(fileLines[5]),                           // totalTime
                    Double.Parse(fileLines[6])                              // rating
                ));
            }

            return foundLogs;
        }

    }
}
