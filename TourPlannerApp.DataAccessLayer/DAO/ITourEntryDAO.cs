using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.DAO
{
    public interface ITourEntryDAO
    {
        TourEntry FindById(int id);
        TourEntry AddNewTour(string name, string description, double distance);
        void DeleteTour(TourEntry tour);
        void UpdateTour(TourEntry tour, string name, string description, double distance);
        IEnumerable<TourEntry> GetTours();
    }
}
