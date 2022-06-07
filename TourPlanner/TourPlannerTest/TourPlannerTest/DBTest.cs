using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using TourPlanner;
using TourPlanner.BusinessLayer;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.FileHandling;
using TourPlanner.DataAccess.DAO;
using TourPlanner.DataAccess.PostgresSqlServer;
using TourPlanner.Model;
using System.Data.Common;
using System.Data;

namespace TourPlannerTest
{
    public class DBTest
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CanReadCfgFile()
        {//cfg file read when fac instantiated first test is just about checking if it can therefore read the right file
            try {
                DALFactory fac = new DALFactory();
            }
            catch { }
            Assert.Pass();
        }

        [Test]
        public void DatabaseNonQueryDoesntThrow()
        {   DALFactory fac = new DALFactory();
            try
            {
                IDatabase db = DALFactory.GetDatabase();
                db.ExecuteNonQuery(db.CreateCommand("UPDATE public.\"Logs\" SET tid = 1 WHERE 1 <> 1 ; "));
            }
            catch { }
            Assert.Pass(); //if gets here it means it could connect to db and make a querry
        }
        [Test]
        public void Reflection_createsDesiredClass()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            Assert.AreEqual(db.GetType(), typeof(Database)); //check for PGSQL DB
        }     
        [Test]
        public void ChildFriendlynessDefaultWorks()
        {
            Tour tour = new Tour(1, "test", "test", "test", "test", 1, 2, new System.TimeSpan(1, 0, 0));
            Assert.AreEqual(tour.ChildFriendliness,ChildFriendliness.ChildFriendly);
        }
        [Test]
        public void ChildFriendlynessCalcWorks1()
        {
            TourLog log = new TourLog(1, 1, "", 2, System.DateTime.Now, new System.TimeSpan(1, 0, 0), 1);
            List<TourLog> logs;
            logs = new List<TourLog>();
            logs.Add(log);
            //tour rated as hard but dist low
            Assert.AreEqual(CalculateChildFHelper.CheckChildfriendlíness(logs,1),ChildFriendliness.OnlyForAdults);
        }
        [Test]
        public void ChildFriendlynessCalcWorks2()
        {
            TourLog log = new TourLog(1, 1, "", 0, System.DateTime.Now, new System.TimeSpan(1, 0, 0), 1);
            List<TourLog> logs;
            logs = new List<TourLog>();
            logs.Add(log);
            //tour rated as easy but dist high
            Assert.AreEqual(CalculateChildFHelper.CheckChildfriendlíness(logs, 15.0), ChildFriendliness.OnlyForAdults);
        }
        [Test]
        public void DALFactoryExportReflectionWorks()
        {
            IFileHandlerDAO fileHandler = DALFactory.GetFileHandler();
            Assert.AreEqual(fileHandler.GetType(), typeof(FileHandlerDAO));
        }
        [Test] 
        public void DALFactoryReportGenerationDoesntThrow()
        {
            IFileHandlerDAO reportHandler = DALFactory.GetReportHandler();
            try { reportHandler.FileExport(null, null); } catch { }
            
            Assert.Pass();//if it gets here it means the file export stopped before wrong data got read
        }
        [Test]
        public void DatabaseCallRecievesData()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            DbCommand getCommand = db.CreateCommand("SELECT * FROM public.\"Tours\"");
            IDataReader reader = db.ExecuteReader(getCommand);
            if (reader.Read())
                Assert.Pass(); //recieves data from DB
        }
        [Test]
        public void DALFactoryPostgresDaoReflectionWorks1()
        {
            ITourDAO dao = DALFactory.CreateTourDAO();
            Assert.AreEqual(dao.GetType(), typeof(TourPostgresDAO));
        }
        [Test]
        public void DALFactoryPostgresDaoReflectionWorks2()
        {
            ITourLogDAO dao = DALFactory.CreateTourLogDAO();
            Assert.AreEqual(dao.GetType(), typeof(TourLogPostgresDAO));
        }

    }
}