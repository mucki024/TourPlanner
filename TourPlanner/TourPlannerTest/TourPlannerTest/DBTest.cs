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

            DALFactory fac = new DALFactory();
            Assert.Pass();
        }

        [Test]
        public void DatabaseNonQueryDoesntThrow()
        {   DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            db.ExecuteNonQuery(db.CreateCommand("UPDATE public.\"Logs\" SET tid = 1 WHERE 1 <> 1 ; "));
            Assert.IsTrue(true); 
        }
        [Test]
        public void Reflection_createsDesiredClass()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            Assert.AreEqual(db.GetType(), typeof(Database)); //check for PGSQL DB
        }
        [Test]
        public void  MapQuestApiConnectionEstablished()
        {
            DALFactory fac = new DALFactory();
            Assert.Fail();
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
            Assert.AreEqual(CalculateChildFHelper.CheckChildfriendlíness(logs, 9.0), ChildFriendliness.OnlyForAdults);
        }
        [Test]
        public void DALFactoryExportReflectionWorks()
        {
            IFileHandlerDAO fileHandler = DALFactory.GetFileHandler();
            Assert.AreEqual(fileHandler, typeof(FileHandlerDAO));
        }
        [Test]
        public void DALFactoryReportReflectionWorks()
        {
            IFileHandlerDAO reportHandler = DALFactory.GetReportHandler();
            Assert.AreEqual(reportHandler, typeof(ReportHandlerDAO));
        }
        [Test]
        public void DatabaseCallWorks()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            //Assert.DoesNotThrow(db.);
        }

    }
}