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
        private static string apiName;
        private static Assembly dalAssembly;
        private static Assembly apiAssembly;
        private static IDatabase database;
        private static IApiAccessDAO apiAccess;
        private static Assembly fileAssembly;
        private static IFileHandlerDAO fileHandler;
        static DALFactory()
        {
            assemblyName = ConfigurationManager.AppSettings["DALSqlAssembly"];
            dalAssembly = Assembly.Load(assemblyName);
            apiName = ConfigurationManager.AppSettings["DALApiAssembly"];
            apiAssembly = Assembly.Load(apiName);
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

        public static IApiAccessDAO GetApi()
        {
            if (apiAccess == null)
                apiAccess = CreateRouteAccess();
            return apiAccess;
        }

        private static IApiAccessDAO CreateRouteAccess()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ApiKey"].ConnectionString;
            string apiClassName = apiName + ".ApiHandlerDAO";
            Type apiClass = apiAssembly.GetType(apiClassName);
            return Activator.CreateInstance(apiClass, new object[] { connectionString }) as IApiAccessDAO; //needs to be done through reflection, cause otherwise child class is not known in this context
        }

        public static IFileHandlerDAO GetFileHandler()
        {
            if (fileHandler == null)
                fileHandler = GetNewFileHandler();
            return fileHandler;
        }

        private static IFileHandlerDAO GetNewFileHandler()
        {
            string fileNamespace = "TourPlanner.DataAccess.FileHandling";
            string fileClassName = fileNamespace + ".FileHandlerDAO";
            fileAssembly = Assembly.Load(fileNamespace);
            Type fileClass = fileAssembly.GetType(fileClassName);
            return Activator.CreateInstance(fileClass, new object[] { }) as IFileHandlerDAO; //needs to be done through reflection, cause otherwise child class is not known in this context
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
