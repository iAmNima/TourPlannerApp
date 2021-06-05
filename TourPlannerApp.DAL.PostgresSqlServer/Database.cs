using log4net;
using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using TourPlannerApp.DataAccessLayer.Common;

namespace TourPlannerApp.DataAccessLayer.PostgresSqlServer
{
    public class Database : IDatabase
    {
        private static readonly ILog Log = LogManager.GetLogger(typeof(Database));

        private string connectionString;

        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }

        // creating a new sql command:
        public DbCommand CreateCommand(string genericCommandText)
        {
            Log.Info("A new SQL command has been created.");
            return new NpgsqlCommand(genericCommandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if (!command.Parameters.Contains(name))
            {
                int index = command.Parameters.Add(new NpgsqlParameter(name, type));
                return index;
            }
            throw new ArgumentException(string.Format("Parameter {0} already exists.", name));
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int index = DeclareParameter(command, name, type);
            command.Parameters[index].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (command.Parameters.Contains(name))
            {
                command.Parameters[name].Value = value;
            }
            else
            {
                throw new ArgumentException(string.Format("Parameter {0} does not exist.", name));
            }
        }

        // connecting to the database with our connection string:
        private DbConnection CreateOpenConnection()
        {
            DbConnection connection = new NpgsqlConnection(this.connectionString);
            Log.Info("A new NpgSql connection has been created");
            connection.Open();
            Log.Info("NpgSql connection has been opened.");
            return connection;
        }

        // execute command, return datareader and close connection:
        public IDataReader ExecuteReader(DbCommand command)
        {
            command.Connection = CreateOpenConnection();
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }

        // execute command and close connection:
        public int ExecuteScalar(DbCommand command)
        {
            Log.Info($"{ command } command has been executed.");
            command.Connection = CreateOpenConnection();
            return Convert.ToInt32(command.ExecuteScalar());
        }

        
    }
}
