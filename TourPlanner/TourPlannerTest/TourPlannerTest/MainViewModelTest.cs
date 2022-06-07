using NUnit.Framework;
using TourPlanner;
using Moq;
using TourPlanner.BusinessLayer;
using TourPlanner.Model;
using System.Collections.Generic;

namespace TourPlannerTest
{
    public class MainViewModelTest
    {
        [SetUp]
        public void Setup()
        {
        }

        
        [Test]
        public void MainViewModel_CheckTourCount()
        {
            var moqFactory = new Mock<WindowFactory>();
            var moqSubWindowTour = new Mock<SubWindowViewTour>();
            var moqSubViewTourDescription= new Mock<SubViewTourDescription>();
            var moqSubSubwindowViewLog= new Mock<SubWindowViewLog>();
            ITourFactory fac = TourFactory.GetInstance();
            var moqSubViewTourLogs= new SubViewTourLogs(moqSubSubwindowViewLog.Object, moqFactory.Object, fac);
            List<Tour> tmp = (List<Tour>)fac.getAllTours();
            
            MainViewModel model = new MainViewModel(moqFactory.Object, moqSubWindowTour.Object, moqSubViewTourDescription.Object, moqSubViewTourLogs, moqSubSubwindowViewLog.Object);

            Assert.IsTrue(model.TourData.Count == tmp.Count);
        }

    }
}