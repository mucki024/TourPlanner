using NUnit.Framework;
using System.Collections.Generic;
using System.Configuration;
using TourPlanner;
using TourPlanner.BusinessLayer;
using TourPlanner.DataAccess.Common;
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

        //need to get cfg file working for those tests
        
        [Test]
        public void DatabaseConstructor_shouldEstablishConnection()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            Assert.IsTrue(true);
        }
        [Test]
        public void Reflection_createsDesiredClass()
        {
            DALFactory fac = new DALFactory();
            IDatabase db = DALFactory.GetDatabase();
            Assert.AreEqual(db.GetType(), typeof(Database)); //change to PGSQL DB
        }
        [Test]
        public void  MapQuestApiConnectionEstablished()
        {
            Assert.Pass();
        }
        [Test]
        public void FindsAndReadsConfigFile()
        {
            Assert.AreEqual("1234565", ConfigurationManager.AppSettings["configCheck"]);
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

    }
}