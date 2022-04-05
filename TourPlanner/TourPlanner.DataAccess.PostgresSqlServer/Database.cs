using Npgsql;
using System;
using System.Data;
using System.Data.Common;
using TourPlanner.DataAccess.Common;

namespace TourPlanner.DataAccess.PostgresSqlServer
{
    public class Database : IDatabase
    {
        private string connectionString;
        public Database(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public DbCommand CreateCommand(string genericCommandText)
        {
            return new NpgsqlCommand(genericCommandText);
        }

        public int DeclareParameter(DbCommand command, string name, DbType type)
        {
            if(!command.Parameters.Contains(name))
            {
                int index = command.Parameters.Add(new NpgsqlParameter(name, type));
                return index;
            }
            throw new ArgumentException(string.Format("Parameter {0} Already Exists!", name));
        }

        public void DefineParameter(DbCommand command, string name, DbType type, object value)
        {
            int index= DeclareParameter(command, name, type);
            command.Parameters[index].Value = value;
        }

        public void SetParameter(DbCommand command, string name, object value)
        {
            if (command.Parameters.Contains(name))
                command.Parameters[name].Value = value;
            throw new ArgumentException(string.Format("Parameter {0} Does Not Exist!", name));
        }
   
        public int ExecuteNonQuery(DbCommand command)
        {
            DbConnection connection = CreateConnection();
            command.Connection = connection;
            return Convert.ToInt32(command.ExecuteScalar());
        }

        public IDataReader ExecuteReader(DbCommand command)
        {
            DbConnection connection = CreateConnection();
            command.Connection = connection;
            return command.ExecuteReader(CommandBehavior.CloseConnection);
        }
        private DbConnection CreateConnection()
        {
            DbConnection connection = new NpgsqlConnection(this.connectionString);
            connection.Open();
            return connection;
        }
    }
}
