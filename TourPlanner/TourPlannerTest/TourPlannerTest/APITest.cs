using NUnit.Framework;
using TourPlanner;
using Moq;
using TourPlanner.DataAccess;
using TourPlanner.DataAccess.DAO;
using TourPlanner.DataAccess.Common;
using System;
using System.Reflection;
using TourPlanner.DataAccess.API;
using TourPlanner.Model;

namespace TourPlannerTest
{
    public class APITest
    {
        private IApiAccessDAO _access;
        [SetUp]
        public void Setup()
        {
            _access = DALFactory.GetApi();
        }
        
        
        [Test]
        public void DALFactory_CreateRouteAccess_ShouldReturnObject()
        {   //test if it possible to access api handler => try catch not neccessary, only for demonstration
            try
            {
                _access = DALFactory.GetApi();
            }
            catch (NullReferenceException ex)
            {
                Assert.Fail("Reflection with Config File did not work: " + ex.Message);
            }
        }

        [Test]
        public void APIAccessDAO_Constructor_ShouldContainAPIConnectionString()
        {   //test if conncectionstring for the API was filled properly
            string apiKey = "xFA4sS7TC6RZk5gGZSr2vmcljK87l692";
            ApiHandlerDAO acc = _access as ApiHandlerDAO;

            FieldInfo fieldInfo = typeof(ApiHandlerDAO).GetField("_connectionApi", BindingFlags.NonPublic | BindingFlags.Instance);
            
            Assert.IsTrue(apiKey == fieldInfo.GetValue(acc).ToString(),"Api Key does not match!");
        }

        [TestCase("Korneuburg", "Wien", TransportType.pedestrian)]
        [TestCase("Wien", "Korneuburg", TransportType.bicycle)]
        public void RouteProcessor_PrepareUrl_ShouldBeIdenticalString(string start, string dest, TransportType trans)
        {   //test if prepared url is valid
            string apiKey = "xFA4sS7TC6RZk5gGZSr2vmcljK87l692";
            string finalUrl = $"http://www.mapquestapi.com/directions/v2/route?key={apiKey}&from={start}&to={dest}&unit=k&routeType={trans.ToString()}";
            RouteProcessor route = new RouteProcessor(apiKey);
            Tour model = new Tour(0,"somename","descr",start,dest,(int)trans,0,System.TimeSpan.Zero);
            
            route.PrepareUrl(model);
            FieldInfo fieldInfo = typeof(RouteProcessor).GetField("_apiUrl", BindingFlags.NonPublic | BindingFlags.Instance);

            Assert.IsTrue(finalUrl == fieldInfo.GetValue(route).ToString(), "Generated Url does not match target Url");
        }

        [TestCase("1112", new int[] { 2, 4, 6, 7 })]
        public void MapProcessor_PrepareUrl_ShouldBeIdenticalString(string sessionID, int[] boundBox)
        {   //test if prepared url is valid
            string apiKey = "xFA4sS7TC6RZk5gGZSr2vmcljK87l692";
            string finalUrl = $"http://www.mapquestapi.com/staticmap/v5/map?key={apiKey}&size=640,480&zoom=11&session={sessionID}&defaultMarker=marker&boundingBox={boundBox[0]},{boundBox[1]},{boundBox[2]},{boundBox[3]}";
            RouteModel model = new RouteModel();
            model.Route.BoundingBox.Ul.Lat = boundBox[0];
            model.Route.BoundingBox.Ul.Lng = boundBox[1];
            model.Route.BoundingBox.Lr.Lat = boundBox[2];
            model.Route.BoundingBox.Lr.Lng = boundBox[3];
            model.Route.SessionId = sessionID;
            MapProcessor map = new MapProcessor(apiKey, 0);

            map.PrepareUrl(model);
            FieldInfo fieldInfo = typeof(MapProcessor).GetField("_apiUrl", BindingFlags.NonPublic | BindingFlags.Instance);
            System.Diagnostics.Debug.Print(fieldInfo.GetValue(map).ToString());
            Assert.IsTrue(finalUrl == fieldInfo.GetValue(map).ToString(), "Generated Url does not match target Url");
        }//var moqSubViewModel = new Mock<SubWindowViewTour>();
    }
}