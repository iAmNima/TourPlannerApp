using log4net;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Linq;
using System.Text;
using TourPlannerApp.DataAccessLayer.Common;
using TourPlannerApp.DataAccessLayer.DAO;
using TourPlannerApp.Models;

namespace TourPlannerApp.DataAccessLayer.PostgresSqlServer
{
    public class TourEntryPostgresDAO : ITourEntryDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(TourEntryPostgresDAO));

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Tours\" WHERE \"tour_ID\"=@Id;";
        private const string SQL_GET_ALL_TOURS = "SELECT * FROM public.\"Tours\";";
        private const string SQL_INSERT_NEW_TOUR = "INSERT INTO public.\"Tours\" (\"name\", \"description\", \"distance\") VALUES (@Name, @Description, @Distance) RETURNING \"tour_ID\";";
        private const string SQL_DELETE_TOUR = "DELETE FROM public.\"Tours\" WHERE \"tour_ID\"=@Id;";
        private const string SQL_UPDATE_TOUR = "UPDATE public.\"Tours\" SET \"name\"=@Name, \"description\"=@Description, \"distance\"=@Distance WHERE \"tour_ID\"=@Id;";

        private IDatabase database;

        public TourEntryPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
        }
        public TourEntryPostgresDAO(IDatabase database)
        {
            this.database = database;
        }

        public TourEntry AddNewTour(string name, string description, double distance)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_TOUR);
            database.DefineParameter(insertCommand, "@Name", DbType.String, name);
            database.DefineParameter(insertCommand, "@Description", DbType.String, description);
            database.DefineParameter(insertCommand, "@Distance", DbType.Double, distance);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public void DeleteTour(TourEntry tour)
        {
            DbCommand deleteCommand = database.CreateCommand(SQL_DELETE_TOUR);
            database.DefineParameter(deleteCommand, "@Id", DbType.Int32, tour.Id);
            database.ExecuteScalar(deleteCommand);
        }

        public void UpdateTour(TourEntry tour, string name, string description, double distance)
        {
            DbCommand UpdateTourCommand = database.CreateCommand(SQL_UPDATE_TOUR);
            database.DefineParameter(UpdateTourCommand, "@Name", DbType.String, name);
            database.DefineParameter(UpdateTourCommand, "@Description", DbType.String, description);
            database.DefineParameter(UpdateTourCommand, "@Distance", DbType.Double, distance);
            database.DefineParameter(UpdateTourCommand, "@Id", DbType.Int32, tour.Id);
            database.ExecuteScalar(UpdateTourCommand);
        }

        public TourEntry FindById(int id)
        {
            DbCommand findByIdcommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdcommand, "@Id", DbType.Int32, id);

            IEnumerable<TourEntry> tours = QueryToursFromDb(findByIdcommand);
            return tours.FirstOrDefault();
        }

        public IEnumerable<TourEntry> GetTours()
        {
            DbCommand getAllToursCommand = database.CreateCommand(SQL_GET_ALL_TOURS);
            return QueryToursFromDb(getAllToursCommand);
        }

        //quering tours from Postgres Database:
        private IEnumerable<TourEntry> QueryToursFromDb(DbCommand command)
        {
            List<TourEntry> tourList = new List<TourEntry>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    tourList.Add(new TourEntry(
                        (int)reader["tour_ID"],
                        (string)reader["name"],
                        (string)reader["description"],
                        (double)reader["distance"]
                    ));
                    Log.Info($"{ tourList.Last().Name } Tour has been recieved from database.");
                }
            }

            return tourList;
        }
    }
}
