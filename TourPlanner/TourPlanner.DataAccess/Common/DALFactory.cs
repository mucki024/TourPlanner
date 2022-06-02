using System;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using TourPlanner.DataAccess.DAO;
using Newtonsoft.Json;
using TourPlanner.Model;
using System.Diagnostics;

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
        private static string configPath;
        private static ConfigData confData;
        static DALFactory()
        {
            LoadConfData();
            assemblyName = confData.DALSqlAssembly;
            dalAssembly = Assembly.Load(assemblyName);
            apiName = confData.DALApiAssembly;
            apiAssembly = Assembly.Load(apiName);
        }
        private static void LoadConfData()
        {
            /*string basdir = System.AppDomain.CurrentDomain.BaseDirectory;
            string projdir = Path.GetFullPath(Path.Combine(basdir, "..", "..", "..", ".."));
            System.Diagnostics.Debug.Print(basdir);*/
            configPath = System.AppDomain.CurrentDomain.BaseDirectory + "\\conf\\conf.json";

            using (StreamReader reader = new StreamReader(configPath))
            {
                string json = reader.ReadToEnd();
                confData = Newtonsoft.Json.JsonConvert.DeserializeObject<ConfigData>(json);
                reader.Dispose();
            }
        }
        public static IDatabase GetDatabase()
        {
            if (database == null)
                database = CreateDatabase();
            return database;
        }
        private static IDatabase CreateDatabase()
        {
            string connectionString = confData.PostgresSqlConnectionString;
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
            string connectionString = confData.ApiKey;
            string apiClassName = apiName + ".ApiHandlerDAO";
            Type apiClass = apiAssembly.GetType(apiClassName);
            return Activator.CreateInstance(apiClass, new object[] { connectionString }) as IApiAccessDAO; //needs to be done through reflection, cause otherwise child class is not known in this context
        }

        public static IFileHandlerDAO GetFileHandler()
        {
            if (fileHandler == null)
                fileHandler = GetNewHandler(".FileHandlerDAO");
            return fileHandler;
        }
        public static IFileHandlerDAO GetReportHandler()
        {
            if (fileHandler == null)
                fileHandler = GetNewHandler(".ReportHandlerDAO");
            return fileHandler;
        }
        private static IFileHandlerDAO GetNewHandler(string desiredClass)
        {
            string fileNamespace = "TourPlanner.DataAccess.FileHandling";
            string fileClassName = fileNamespace + desiredClass;
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
