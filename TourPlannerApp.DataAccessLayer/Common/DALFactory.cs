using log4net;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Reflection;
using System.Text;
using TourPlannerApp.DataAccessLayer.DAO;

namespace TourPlannerApp.DataAccessLayer.Common
{
    public class DALFactory
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(DALFactory));
        private static bool useFileSystem = false;
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;
        private static IFileAccess fileAccess;

        static DALFactory()
        {
            useFileSystem = bool.Parse(ConfigurationManager.AppSettings["useFileSystem"]);
            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            if (useFileSystem)
            {
                assemblyName = ConfigurationManager.AppSettings["DALFileAssembly"];
            }
            dalAssembly = Assembly.Load(assemblyName);
        }

        public static IDatabase GetDatabase()
        {
            if (database == null)
            {
                database = CreateDatabase();
            }
            return database;
        }

        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSQLConnectionString"].ConnectionString;
            Log.Info("Database is created using our connection string.");
            return CreateDatabase(connectionString);
        }

        // create database with our connection String:
        private static IDatabase CreateDatabase(string connectionString)
        {
            string className = assemblyName + ".Database";
            Type zoneType = dalAssembly.GetType(className);

            return Activator.CreateInstance(zoneType,
              new object[] { connectionString }) as IDatabase;
        }

        public static IFileAccess GetFileAccess()
        {
            if (fileAccess == null)
            {
                fileAccess = CreateFileAccess();
            }
            return fileAccess;
        }

        private static IFileAccess CreateFileAccess()
        {
            string startFolder = ConfigurationManager.ConnectionStrings["StartFolderFilePath"].ConnectionString;
            return CreateFileAccess(startFolder);
        }

        private static IFileAccess CreateFileAccess(string startFolder)
        {
            string databaseClassName = assemblyName + ".FileAccess";
            Type dbClass = dalAssembly.GetType(databaseClassName);

            return Activator.CreateInstance(dbClass,
              new object[] { startFolder }) as IFileAccess;
        }

        // create TourEntry sql/file DAO object
        public static ITourEntryDAO CreateTourEntryDAO()
        {
            string className = assemblyName + ".TourEntryPostgresDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".TourEntryFileDAO";
            }

            Type zoneType = dalAssembly.GetType(className);
            Log.Info("an instance of TourEntryDAO has been created.");
            return Activator.CreateInstance(zoneType) as ITourEntryDAO;
        }

        // create LogEntry sql/file DAO object
        public static ILogEntryDAO CreateLogEntryDAO()
        {
            string className = assemblyName + ".LogEntryPostgresDAO";
            if (useFileSystem)
            {
                className = assemblyName + ".LogEntryFileDAO";
            }

            Type zoneType = dalAssembly.GetType(className);
            Log.Info("an instance of LogEntryDAO has been created.");
            return Activator.CreateInstance(zoneType) as ILogEntryDAO;
        }

    }
}
