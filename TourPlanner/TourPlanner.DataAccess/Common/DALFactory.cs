using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TourPlanner.DataAccess.DAO;

namespace TourPlanner.DataAccess.Common
{
    public class DALFactory
    {
        private static string assemblyName;
        private static Assembly dalAssembly;
        private static IDatabase database;
        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
        }
        public static IDatabase GetDatabase()
        {
            if (database == null)
                database = CreateDatabase();
            return database;
        }
        private static IDatabase CreateDatabase()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["PostgresSqlConnectionString"].ConnectionString;
            return CreateDatabase(connectionString);
        }
        private static IDatabase CreateDatabase(string connectionString)
        {
            string databaseClassName = assemblyName + ".Database";
            Type dbClass = dalAssembly.GetType(databaseClassName);
            return Activator.CreateInstance(dbClass,new object[] {connectionString}) as IDatabase;
        }
        public static ITourDAO CreateTourDAO()
        {
            string className = assemblyName + ".TourPostgresDAO";
            Type TourType = dalAssembly.GetType(className);
            return Activator.CreateInstance(TourType) as ITourDAO;
        }
        public static ITourLogDAO CreateTourLogDAO()
        {
            string className = assemblyName + ".TourLogPostgresDAO";
            Type TourLogType = dalAssembly.GetType(className);
            return Activator.CreateInstance(TourLogType) as ITourLogDAO;
        }
    }
}
