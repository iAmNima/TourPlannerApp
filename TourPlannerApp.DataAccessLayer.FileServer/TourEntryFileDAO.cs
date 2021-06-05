using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TourPlannerApp.DataAccessLayer.Common;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.FileServer
{
    public class TourEntryFileDAO : ITourEntryDAO
    {
        private IFileAccess fileAccess;

        public TourEntryFileDAO()
        {
            this.fileAccess = DALFactory.GetFileAccess();
        }
        public TourEntryFileDAO(IFileAccess fileAccess)
        {
            this.fileAccess = fileAccess;
        }

        public TourEntry AddNewTour(string name, string description, double distance)
        {
            int id = fileAccess.CreateNewTourEntryFile(name, description, distance);
            return new TourEntry(id, name, description, distance);
        }

        public void DeleteTour(TourEntry tour)
        {
            throw new NotImplementedException();
        }

        public TourEntry FindById(int id)
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.SearchFiles(id.ToString(), EntryTypes.TourEntry);
            return QueryFromFileSystem(foundFiles).FirstOrDefault();
        }

        public IEnumerable<TourEntry> GetTours()
        {
            IEnumerable<FileInfo> foundFiles = fileAccess.GetAllFiles(EntryTypes.TourEntry);
            return QueryFromFileSystem(foundFiles);
        }

        public void UpdateTour(TourEntry tour, string name, string description, double distance)
        {
            throw new NotImplementedException();
        }

        private IEnumerable<TourEntry> QueryFromFileSystem(IEnumerable<FileInfo> foundFiles)
        {
            List<TourEntry> foundTourEntries = new List<TourEntry>();

            foreach (FileInfo file in foundFiles)
            {
                string[] fileLines = File.ReadAllLines(file.FullName);
                foundTourEntries.Add(new TourEntry(
                    int.Parse(fileLines[0]),        // id
                    fileLines[1],                   // name
                    fileLines[2],                   // description
                    Double.Parse(fileLines[3])      // distance
                ));
            }

            return foundTourEntries;
        }

         


    }
}
