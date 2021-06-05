using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;

namespace TourPlannerApp.BusinessLayer
{
    public interface ITourPlannerAppFactory
    {
        ITourEntryDAO tourEntryDAO { get; set; }
        String RouteImagesFolder { get; set; }

        IEnumerable<TourEntry> GetTours();

        IEnumerable<LogEntry> GetLogsByTour(TourEntry tour);

        IEnumerable<TourEntry> Search(string itemName, bool caseSensitive = false);

        TourEntry CreateTour(string name, string description, double distance);

        void DeleteTour(TourEntry tour);

        void UpdateTour(TourEntry tour, string name, string description, double distance);

        LogEntry CreateLog(TourEntry log_tourEntry,
            DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating);

        void UpdateLog(LogEntry log, DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating);

        void DeleteLog(LogEntry log);

        public void SaveRouteImageFromApi(string from, string to, string tourName);

        public string GetRouteImgPath(string tourName);

        public void GenerateReport(TourEntry tour, IEnumerable<LogEntry> logs);

        public string GetReportFolderPath(string tourName);
    }
}
