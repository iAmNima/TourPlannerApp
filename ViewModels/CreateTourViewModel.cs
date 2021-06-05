using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Input;
using TourPlannerApp.BusinessLayer;
using TourPlannerApp.Models;

namespace TourPlannerApp.ViewModels
{
    public class CreateTourViewModel : ViewModelBase
    {

        private ITourPlannerAppFactory tourPlannerAppFactory;
        private ICommand addTourCommand;
        private string tourName;
        private string tourFrom;
        private string tourTo;
        private string tourDescription;
        private double tourDistance;
        public ICommand AddTourCommand => addTourCommand ??= new RelayCommand(AddNewTour);

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

        public CreateTourViewModel()
        {
            this.tourPlannerAppFactory = TourPlannerAppFactory.GetInstance();
        }

        private void AddNewTour(object commandParameter)
        {
            TourEntry createdTour = this.tourPlannerAppFactory.CreateTour(TourName, TourDescription, TourDistance);
            //using from & to parameters to found the route image for this tour:
            this.tourPlannerAppFactory.SaveRouteImageFromApi(TourFrom, TourTo, TourName); //TODO: tourName needs to be added
            //FillListView();
            MessageBox.Show("New Tour got added! go back to Home!");
        }
    }
}
