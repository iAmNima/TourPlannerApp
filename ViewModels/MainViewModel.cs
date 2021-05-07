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

[assembly: log4net.Config.XmlConfigurator(ConfigFile ="App.config", Watch =true)] 

namespace TourPlannerApp.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(MainViewModel));
        

        private ITourPlannerAppFactory tourPlannerAppFactory;
        private TourEntry currentTour;
        private string searchName;
        private string tourName;
        private string tourFrom;
        private string tourTo;
        private string tourDescription;
        private double tourDistance;
        private string currentRouteImgPath;

        private ICommand searchCommand;
        private ICommand addTourCommand;

        

        public ICommand SearchCommand => searchCommand ??= new RelayCommand(Search);
        public ICommand AddTourCommand => addTourCommand ??= new RelayCommand(AddNewTour);


        public ObservableCollection<TourEntry> Tours { get; set; }


        public MainViewModel()
        {
            this.tourPlannerAppFactory = TourPlannerAppFactory.GetInstance();
            InitListView();
            log4net.Config.XmlConfigurator.Configure();
            Log.Info("hi");
        }

        private void InitListView()
        {
            Tours = new ObservableCollection<TourEntry>();
            FillListView();
            Log.Debug("ooonneee");
        }

        private void FillListView()
        {
            Tours.Clear();
            foreach (TourEntry tour in this.tourPlannerAppFactory.GetTours())
            {
                Tours.Add(tour);
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

        public TourEntry CurrentTour
        {
            get { return currentTour; }
            set
            {
                if((currentTour != value) && (value != null)) {
                    currentTour = value;
                    RaisePropertyChangedEvent(nameof(CurrentTour));
                    currentRouteImgPath = this.tourPlannerAppFactory.GetRouteImgPath(currentTour.Name);
                    RaisePropertyChangedEvent(nameof(CurrentRouteImgPath));
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

        public void Search(object commandParameter)
        {
            IEnumerable<TourEntry> foundTours = this.tourPlannerAppFactory.Search(SearchName);
            Tours.Clear();
            SearchName = "";
            Log.Debug("search method is called.");
            foreach(TourEntry tour in foundTours)
            {
                Tours.Add(tour);
            }
        }

        //method: to add a new tour
        private void AddNewTour(object commandParameter)
        {
            TourEntry createdTour = this.tourPlannerAppFactory.CreateTour(TourName, TourDescription, TourDistance);
            //using from & to parameters to found the route image for this tour:
            this.tourPlannerAppFactory.SaveRouteImageFromApi(TourFrom, TourTo, TourName); //TODO: tourName needs to be added
            FillListView();
            MessageBox.Show("New Tour got added! go back to Home!");
        }
    }
}
