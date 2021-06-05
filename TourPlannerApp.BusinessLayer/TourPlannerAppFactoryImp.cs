using log4net;
using QuestPDF;
using QuestPDF.Fluent;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlannerApp.BusinessLayer.QuestPdf;
using TourPlannerApp.DataAccessLayer;
using TourPlannerApp.DataAccessLayer.Common;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;

namespace TourPlannerApp.BusinessLayer
{
    internal class TourPlannerAppFactoryImp : ITourPlannerAppFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TourPlannerAppFactoryImp));
        public String RouteImagesFolder { get; set; }
        public String ReportPdfsFolder { get; set; }
        public ITourEntryDAO tourEntryDAO { get; set; }
        public ILogEntryDAO logEntryDAO { get; set; }


        public TourPlannerAppFactoryImp()
        {
            RouteImagesFolder = ConfigurationManager.AppSettings["RouteImagesFolder"];
            ReportPdfsFolder = ConfigurationManager.AppSettings["ReportPdfsFolder"];
            tourEntryDAO = DALFactory.CreateTourEntryDAO();
            logEntryDAO = DALFactory.CreateLogEntryDAO();
            Log.Info("TourPlannerAppFactoryImp constructor without parameter is called.");
        }

        // next two constructors are for testing purposes.
        public TourPlannerAppFactoryImp(ITourEntryDAO tourEntryDAO)
        {
            RouteImagesFolder = ConfigurationManager.AppSettings["RouteImagesFolder"];
            ReportPdfsFolder = ConfigurationManager.AppSettings["ReportPdfsFolder"];
            this.tourEntryDAO = tourEntryDAO;
            Log.Info("TourPlannerAppFactoryImp constructor with parameter is called.");
        }
        public TourPlannerAppFactoryImp(ILogEntryDAO logEntryDAO)
        {
            RouteImagesFolder = ConfigurationManager.AppSettings["RouteImagesFolder"];
            ReportPdfsFolder = ConfigurationManager.AppSettings["ReportPdfsFolder"];
            this.logEntryDAO = logEntryDAO;
            Log.Info("TourPlannerAppFactoryImp constructor with parameter is called.");
        }


        public IEnumerable<TourEntry> GetTours()
        {
            // ITourEntryDAO tourEntryDAO = DALFactory.CreateTourEntryDAO();
            Log.Info("Tours info recived from tourEntryDAO in BL");
            return tourEntryDAO.GetTours();
        }

        public IEnumerable<TourEntry> Search(string itemName, bool caseSensitive = false)
        {

            IEnumerable<TourEntry> tours = GetTours();
            if(caseSensitive)
            {
                return tours.Where(x => x.Name.Contains(itemName)).ToList();
            }
            tours = tours.Where(x => x.Name.ToLower().Contains(itemName.ToLower())).ToList();
            return tours;
        }

        public TourEntry CreateTour(string name, string description, double distance)
        {
            Log.Info("CreateTour method of our BL factory class is called");
            // ITourEntryDAO tourEntryDao = DALFactory.CreateTourEntryDAO();
            return tourEntryDAO.AddNewTour(name, description, distance);
        }

        public void DeleteTour(TourEntry tour)
        {          
            //delete a Tour:
            tourEntryDAO.DeleteTour(tour);
            //and delete its Logs:
            logEntryDAO.DeleteLogsByTour(tour);
            //and delete its Route Image:
            //MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            //mapQuestApiProcessor.DeleteRouteImage(tour);

        }
        public void UpdateTour(TourEntry tour, string name, string description, double distance)
        {
            tourEntryDAO.UpdateTour(tour, name, description, distance);
        }

        public void DeleteLog(LogEntry log)
        {
            logEntryDAO.DeleteLog(log);
        }

        public void UpdateLog(LogEntry log, DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating)
        {
            logEntryDAO.UpdateLog(log, logDate, report, distance, totalTime, rating);
        }

        public LogEntry CreateLog(TourEntry log_tourEntry,
            DateTime logDate,
            string report,
            double distance,
            TimeSpan totalTime,
            double rating)
        {
            Log.Info("CreateLog method of our BL factory class is called");
            return logEntryDAO.AddNewLog(log_tourEntry, logDate, report, distance, totalTime, rating);
        }

        public async void SaveRouteImageFromApi(string from, string to, string tourName)
        {
            Log.Info("SaveRouteImageFromApi method of our BL factory class is called");
            MapQuestApiProcessor mapQuestApiProcessor = new MapQuestApiProcessor();
            string directionsApiurl = mapQuestApiProcessor.CreateDirectionApiUrl(from, to, tourName);
            //calling the Directions Api:
            Tuple<string, string> t = await mapQuestApiProcessor.DirectionsAPI(directionsApiurl, tourName);
            string staticMapApiUrl = mapQuestApiProcessor.CreateStaticMapApiUrl(t.Item1, t.Item2);
            //calling the StaticMap Api and saving the image:
            mapQuestApiProcessor.StaticMapAPI(staticMapApiUrl, tourName);
        }

        public string GetRouteImgPath(string tourName)
        {
            Log.Info("GetRouteImgPath method of our BL factory class is called");
            string path = RouteImagesFolder + "\\" + tourName + ".png";
            return path;
        }

        public IEnumerable<LogEntry> GetLogsByTour(TourEntry tour)
        {
            return logEntryDAO.GetLogsByTour(tour);
        }

        public void GenerateReport(TourEntry tour, IEnumerable<LogEntry> logs)
        {
            string filePath = GetReportFolderPath(tour.Name);
            PdfModel model = QuestPdfDataSource.GetTourAndLogsInfo(tour, logs);
            var document = new QuestpdfTemplate(model);
            document.GeneratePdf(filePath);
        }

        public string GetReportFolderPath(string tourName)
        {
            string filePath = ReportPdfsFolder + "\\" + tourName + ".pdf";
            return filePath;
        }

    }
}
