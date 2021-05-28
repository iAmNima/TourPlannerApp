using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using TourPlannerApp.BusinessLayer;
using TourPlannerApp.Models;
using log4net;
using log4net.Config;
using System.Windows;
using TourPlannerApp.DataAccessLayer.DAO;

[assembly: log4net.Config.XmlConfigurator(ConfigFile ="App.config", Watch =true)] 

namespace TourPlannerApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainViewModel));
        

        private ITourPlannerAppFactory tourPlannerAppFactory;
        private TourEntry currentTour;
        private LogEntry currentLog;
        private string searchName;
        private string tourName;
        private string tourFrom;
        private string tourTo;
        private string tourDescription;
        private double tourDistance;
        private string currentRouteImgPath;
        private DateTime logDate;
        private string logReport;
        private double logDistance;
        private TimeSpan logTotalTime;
        private double logRating;

        private ICommand searchCommand;
        private ICommand addTourCommand;
        private ICommand addLogCommand;
        private ICommand generateReportCommand;
        private ICommand deleteTourCommand;
        private ICommand deleteLogCommand;
        private ICommand updateTourCommand;
        private ICommand updateLogCommand;
        private ICommand editTourCommand;


        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand AddTourCommand => addTourCommand ??= new RelayCommand(AddNewTour);
        public ICommand AddLogCommand => addLogCommand ??= new RelayCommand(AddNewLog);
        public ICommand GenerateReportCommand => generateReportCommand ??= new RelayCommand(GenerateReport);
        public ICommand DeleteTourCommand => deleteTourCommand ??= new RelayCommand(DeleteTour);
        public ICommand DeleteLogCommand => deleteLogCommand ??= new RelayCommand(DeleteLog);
        public ICommand UpdateTourCommand => updateTourCommand ??= new RelayCommand(UpdateTour);
        public ICommand UpdateLogCommand => updateLogCommand ??= new RelayCommand(UpdateLog);
        public ICommand EditTourCommand => editTourCommand ??= new RelayCommand(EditTour);
        public ObservableCollection<TourEntry> Tours { get; set; }
        public ObservableCollection<LogEntry> Logs { get; set; }

        public MainViewModel()
        {
            this.tourPlannerAppFactory = TourPlannerAppFactory.GetInstance();
            InitListView();
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("MainViewModel created.");

        }
        public MainViewModel(ITourEntryDAO tourEntryDAO)
        {
            this.tourPlannerAppFactory = TourPlannerAppFactory.GetInstance(tourEntryDAO);
            InitListView();
            log4net.Config.XmlConfigurator.Configure();
        }

        public void InitListView()
        {
            Tours = new ObservableCollection<TourEntry>();
            Logs = new ObservableCollection<LogEntry>();
            FillListView();
            Log.Info("Our Tour ListView initilized.");
        }

        public void FillListView()
        {
            Tours.Clear();
            foreach (TourEntry tour in this.tourPlannerAppFactory.GetTours())
            {
                Tours.Add(tour);
                Log.Info($"{ tour.Name } Tour has been added to our Tour list.");
            }
            Log.Info("Tours are filled with the database data.");
        }

        public void FillLogs()
        {
            //geting logs for this current Tour:
            Logs.Clear();
            foreach (LogEntry log in tourPlannerAppFactory.GetLogsByTour(CurrentTour))
            {
                Logs.Add(log);
                //Log.Info($"{ tour.Name } Tour has been added to our Tour list.");
            }
        }

        public string SearchName
        {
            get { return searchName; }
            set
            {
                if(searchName != value)
                {
                    searchName = value;
                    RaisePropertyChangedEvent(nameof(SearchName));
                }
            }
        }

        //** Tour info for adding a new tour **//
        public string TourName
        {
            get { return tourName; }
            set
            {
                if (tourName != value)
                {
                    tourName = value;
                    RaisePropertyChangedEvent(nameof(TourName));
                }
            }
        }
        public string TourFrom
        {
            get { return tourFrom; }
            set
            {
                if (tourFrom != value)
                {
                    tourFrom = value;
                    RaisePropertyChangedEvent(nameof(TourFrom));
                }
            }
        }
        public string TourTo
        {
            get { return tourTo; }
            set
            {
                if (tourTo != value)
                {
                    tourTo = value;
                    RaisePropertyChangedEvent(nameof(TourTo));
                }
            }
        }
        public string TourDescription
        {
            get { return tourDescription; }
            set
            {
                if (tourDescription != value)
                {
                    tourDescription = value;
                    RaisePropertyChangedEvent(nameof(TourDescription));
                }
            }
        }

        public double TourDistance
        {
            get { return tourDistance; }
            set
            {
                if (tourDistance != value)
                {
                    tourDistance = value;
                    RaisePropertyChangedEvent(nameof(TourDistance));
                }
            }
        }


        //** Log info for adding a new log **//
        public DateTime LogDate
        {
            get { return logDate; }
            set
            {
                if (logDate != value)
                {
                    logDate = value;
                    RaisePropertyChangedEvent(nameof(LogDate));
                }
            }
        }
        public string LogReport
        {
            get { return logReport; }
            set
            {
                if (logReport != value)
                {
                    logReport = value;
                    RaisePropertyChangedEvent(nameof(LogReport));
                }
            }
        }
        public double LogDistance
        {
            get { return logDistance; }
            set
            {
                if (logDistance != value)
                {
                    logDistance = value;
                    RaisePropertyChangedEvent(nameof(LogDistance));
                }
            }
        }
        public TimeSpan LogTotalTime
        {
            get { return logTotalTime; }
            set
            {
                if (logTotalTime != value)
                {
                    logTotalTime = value;
                    RaisePropertyChangedEvent(nameof(LogTotalTime));
                }
            }
        }
        public double LogRating
        {
            get { return logRating; }
            set
            {
                if (logRating != value)
                {
                    logRating = value;
                    RaisePropertyChangedEvent(nameof(LogRating));
                }
            }
        }

        public TourEntry CurrentTour
        {
            get { return currentTour; }
            set
            {
                if((currentTour != value) && (value != null)) {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    Log.Info($"{ CurrentTour.Name } Tour has been set as our current Tour.");
                    currentRouteImgPath = this.tourPlannerAppFactory.GetRouteImgPath(currentTour.Name);
                    Log.Info("Route Image Path of currentTour is set.");
                    RaisePropertyChangedEvent(nameof(CurrentRouteImgPath));
                    FillLogs();
                }
            } 
        }
        public LogEntry CurrentLog
        {
            get { return currentLog; }
            set
            {
                if ((currentLog != value) && (value != null))
                {
                    currentLog = value;
                    RaisePropertyChangedEvent(nameof(CurrentLog));
                }
            }
        }

        public string CurrentRouteImgPath
        {
            get
            {
                return currentRouteImgPath;
            }
            set
            {
                if (currentRouteImgPath != value)
                {
                    currentRouteImgPath = value;
                    RaisePropertyChangedEvent(nameof(CurrentRouteImgPath));
                }
            }
        }


        //Search method:
        public void Search(object commandParameter)
        {
            IEnumerable<TourEntry> foundTours = this.tourPlannerAppFactory.Search(SearchName);
            Tours.Clear();
            SearchName = "";
            Log.Info("search method of MainVM is called.");
            foreach (TourEntry tour in foundTours)
            {
                Tours.Add(tour);
                Log.Info($"{ tour.Name } Tour has been added to our Tour list.");
            }
        }


        //Tour methods:
        private void AddNewTour(object commandParameter)
        {
            Log.Info("AddNewTour method of our MainVM is called.");
            TourEntry createdTour = this.tourPlannerAppFactory.CreateTour(TourName, TourDescription, TourDistance);
            //using from & to parameters to found the route image for this tour:
            this.tourPlannerAppFactory.SaveRouteImageFromApi(TourFrom, TourTo, TourName); //TODO: tourName needs to be added
            FillListView();
            MessageBox.Show("New Tour got added! go back to Home!");
        }
        private void DeleteTour(object commandParameter)
        {
            tourPlannerAppFactory.DeleteTour(CurrentTour);
            FillListView();
            MessageBox.Show("Tour Deleted!");
        }
        private void UpdateTour(object commandParameter)
        {
            tourPlannerAppFactory.UpdateTour(CurrentTour, TourName, TourDescription, TourDistance);
            FillListView();
            MessageBox.Show("Tour Updated!");
        }
        

        //Log methods:
        private void AddNewLog(object commandParameter)
        {
            this.tourPlannerAppFactory.CreateLog(CurrentTour, LogDate, LogReport, LogDistance, LogTotalTime, LogRating);
            MessageBox.Show("New Log got Added!");
            FillLogs();
        }
        private void DeleteLog(object commandParameter)
        {
            tourPlannerAppFactory.DeleteLog(CurrentLog);
            FillLogs();
        }
        private void UpdateLog(object commandParameter)
        {
            tourPlannerAppFactory.UpdateLog(CurrentLog, LogDate, LogReport, LogDistance, LogTotalTime, LogRating);
            FillLogs();
            MessageBox.Show("Log Updated!");
        }


        // creating a pdf report including the tour and its logs info:
        private void GenerateReport(object commandParameter)
        {
            this.tourPlannerAppFactory.GenerateReport(CurrentTour, Logs);
            MessageBox.Show("New Report created!");
        }

        private void EditTour(object commandParameter)
        {
            CreateTourWindow editWindow = new CreateTourWindow();
            editWindow.ShowDialog();
            FillListView();
        }
    }
}
