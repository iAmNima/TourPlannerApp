using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.BusinessLayer.QuestPdf
{
    public static class QuestPdfDataSource
    {
        // this class includes a static class that takes currentTour and Logs as parameters:
        public static PdfModel GetTourAndLogsInfo(TourEntry tour, IEnumerable<LogEntry> logs)
        {

            string path = "C:\\Users\\nimab\\OneDrive\\Desktop\\semester 4\\WPF projects\\TourPlannerApp\\RouteImgs" + "\\" + tour.Name + ".png";

            return new PdfModel
            {
                TourInfo = tour,
                LogsInfo = logs,
                ImageBytes = File.ReadAllBytes(path)
            };

        }
    }
}
