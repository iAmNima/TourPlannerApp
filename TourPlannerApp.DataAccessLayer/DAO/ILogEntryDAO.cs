using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.DAO
{
    public interface ILogEntryDAO
    {
        LogEntry FindById(int id);
        LogEntry AddNewLog(TourEntry log_tourEntry,
            DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating);
        void DeleteLogsByTour(TourEntry tour);
        void DeleteLog(LogEntry log);
        void UpdateLog(LogEntry log, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating);
        IEnumerable<LogEntry> GetLogsByTour(TourEntry tour);
    }
}
