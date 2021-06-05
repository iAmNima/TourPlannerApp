using System;
namespace TourPlannerApp.Models
{
    public class LogEntry
    {
        public int Id { get; set; }
        public TourEntry Log_tourEntry { get; set; }
        public DateTime LogDate { get; set; }
        public string Report { get; set; }
        public double Distance { get; set; }
        public TimeSpan TotalTime { get; set; }
        public double Rating { get; set; }


        public LogEntry(
            int id,
            TourEntry log_tourEntry,
            DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating
            )
        {
            this.Id = id;
            this.Log_tourEntry = log_tourEntry;
            this.LogDate = logDate;
            this.Report = report;
            this.Distance = distance;
            this.TotalTime = totalTime;
            this.Rating = rating;
        }
    }
}
