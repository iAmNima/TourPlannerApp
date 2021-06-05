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
    public class LogEntryPostgresDAO : ILogEntryDAO
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(LogEntryPostgresDAO));

        private const string SQL_FIND_BY_ID = "SELECT * FROM public.\"Logs\" WHERE \"log_ID\"=@Id";
        private const string SQL_FIND_BY_TOUR = "SELECT * FROM public.\"Logs\" WHERE \"tour_ID_FK\"=@TourId";
        private const string SQL_INSERT_NEW_LOG = "INSERT INTO public.\"Logs\" (\"log_date\", \"report\", \"distance\", \"total_time\", \"rating\", \"tour_ID_FK\") VALUES (@LogDate, @Report, @Distance, @TotalTime, @Rating, @TourId);";
        private const string SQL_DELETE_BY_TOUR = "DELETE FROM public.\"Logs\" WHERE \"tour_ID_FK\"=@TourId";
        private const string SQL_DELETE_LOG = "DELETE FROM public.\"Logs\" WHERE \"log_ID\"=@Id";
        private const string SQL_UPDATE_LOG = "UPDATE public.\"Logs\" SET \"log_date\"=@LogDate, \"report\"=@Report, \"distance\"=@Distance, \"total_time\"=@TotalTime, \"rating\"=@Rating WHERE \"log_ID\"=@Id";

        private IDatabase database;
        private ITourEntryDAO tourEntryDAO;

        public LogEntryPostgresDAO()
        {
            this.database = DALFactory.GetDatabase();
            this.tourEntryDAO = DALFactory.CreateTourEntryDAO();
        }
        public LogEntryPostgresDAO(IDatabase database, ITourEntryDAO tourEntryDAO)
        {
            this.database = database;
            this.tourEntryDAO = tourEntryDAO;
        }

        public LogEntry AddNewLog(TourEntry log_tourEntry, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating)
        {
            DbCommand insertCommand = database.CreateCommand(SQL_INSERT_NEW_LOG);
            database.DefineParameter(insertCommand, "@LogDate", DbType.Date, logDate);
            database.DefineParameter(insertCommand, "@Report", DbType.String, report);
            database.DefineParameter(insertCommand, "@Distance", DbType.Double, distance);
            database.DefineParameter(insertCommand, "@TotalTime", DbType.Time, totalTime);
            database.DefineParameter(insertCommand, "@Rating", DbType.Double, rating);
            database.DefineParameter(insertCommand, "@TourId", DbType.Int32, log_tourEntry.Id);

            return FindById(database.ExecuteScalar(insertCommand));
        }

        public void DeleteLogsByTour(TourEntry tour)
        {
            DbCommand DeleteLogsByTourCommand = database.CreateCommand(SQL_DELETE_BY_TOUR);
            database.DefineParameter(DeleteLogsByTourCommand, "@TourId", DbType.Int32, tour.Id);
            database.ExecuteScalar(DeleteLogsByTourCommand);
        }

        public void DeleteLog(LogEntry log)
        {
            DbCommand DeleteLogCommand = database.CreateCommand(SQL_DELETE_LOG);
            database.DefineParameter(DeleteLogCommand, "@Id", DbType.Int32, log.Id);
            database.ExecuteScalar(DeleteLogCommand);
        }

        public void UpdateLog(LogEntry log, DateTime logDate, string report, double distance, TimeSpan totalTime, double rating)
        {
            DbCommand UpdateLogCommand = database.CreateCommand(SQL_UPDATE_LOG);
            database.DefineParameter(UpdateLogCommand, "@LogDate", DbType.Date, logDate);
            database.DefineParameter(UpdateLogCommand, "@Report", DbType.String, report);
            database.DefineParameter(UpdateLogCommand, "@Distance", DbType.Double, distance);
            database.DefineParameter(UpdateLogCommand, "@TotalTime", DbType.Time, totalTime);
            database.DefineParameter(UpdateLogCommand, "@Rating", DbType.Double, rating);
            database.DefineParameter(UpdateLogCommand, "@Id", DbType.Int32, log.Id);
            database.ExecuteScalar(UpdateLogCommand);
        }

        public LogEntry FindById(int id)
        {
            DbCommand findByIdCommand = database.CreateCommand(SQL_FIND_BY_ID);
            database.DefineParameter(findByIdCommand, "@Id", DbType.Int32, id);

            IEnumerable<LogEntry> logList = QueryLogsFromDb(findByIdCommand);
            return logList.FirstOrDefault();
        }


        public IEnumerable<LogEntry> GetLogsByTour(TourEntry tour)
        {
            DbCommand getLogsByTourCommand = database.CreateCommand(SQL_FIND_BY_TOUR);
            database.DefineParameter(getLogsByTourCommand, "@TourId", DbType.Int32, tour.Id);

            return QueryLogsFromDb(getLogsByTourCommand);
        }

        private IEnumerable<LogEntry> QueryLogsFromDb(DbCommand command)
        {
            List<LogEntry> logList = new List<LogEntry>();

            using (IDataReader reader = database.ExecuteReader(command))
            {
                while (reader.Read())
                {
                    logList.Add(new LogEntry(
                       (int)reader["log_ID"],
                       tourEntryDAO.FindById((int)reader["tour_ID_FK"]),
                       (DateTime)reader["log_date"],
                       (string)reader["report"],
                       (double)reader["distance"],
                       (TimeSpan)reader["total_time"],
                       (double)reader["rating"]
                   ));
                }
            }

            return logList;
        }
    }
}
