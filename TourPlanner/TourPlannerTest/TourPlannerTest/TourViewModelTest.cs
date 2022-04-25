using NUnit.Framework;
using TourPlanner;
using Moq;

namespace TourPlannerTest
{
    public class TourViewModelTest
    {
        private SubWindowViewTour _model;
        [SetUp]
        public void Setup()
        {
            _model = new SubWindowViewTour();
        }
        
        /*
        [Test]
        public void SubWindowViewTour_shouldFireEvent()
        {
            bool didFire = false;
            TourWindow window = new TourWindow();
            _model.OnSubmitClicked += (s, param) =>
            {
                didFire = true;
            };
            Assert.IsTrue(didFire);
        }
        */
        //test behaviour for add button 
    }
}