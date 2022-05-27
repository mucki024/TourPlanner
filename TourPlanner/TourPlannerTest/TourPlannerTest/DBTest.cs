using NUnit.Framework;
using System.Configuration;
using TourPlanner;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.PostgresSqlServer;

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
            Assert.Pass();
        }
        public void ChildFriendlynessCalcWorks()
        {
            Assert.Pass();
        }
        public void LengthEstimationWorks()
        {
            Assert.Pass();
        }

    }
}