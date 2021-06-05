using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.DataAccessLayer.DAO;

namespace TourPlannerApp.BusinessLayer
{
    public static class TourPlannerAppFactory
    {
        private static ITourPlannerAppFactory instance;

        public static ITourPlannerAppFactory GetInstance()
        {
            if(instance == null)
            {
                instance = new TourPlannerAppFactoryImp();
            }
            return instance;
        }
        public static ITourPlannerAppFactory GetInstance(ITourEntryDAO tourEntryDAO)
        {
            
            instance = new TourPlannerAppFactoryImp(tourEntryDAO);
            
            return instance;
        }
        public static ITourPlannerAppFactory GetInstance(ILogEntryDAO logEntryDAO)
        {
            
            instance = new TourPlannerAppFactoryImp(logEntryDAO);
            return instance;
        }

    }
}
