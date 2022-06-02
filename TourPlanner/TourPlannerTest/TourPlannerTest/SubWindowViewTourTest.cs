using NUnit.Framework;
using TourPlanner;
using Moq;
using System;
using System.Diagnostics;
using TourPlanner.Model;
using System.Reflection;

namespace TourPlannerTest
{
    public class SubWindowViewTourTest
    {
        private SubWindowViewTour _viewModel;
        [SetUp]
        public void Setup()
        {
            _viewModel = new SubWindowViewTour();
        }

        [Test]
        public void SubWindowViewTour_CreateTour_shouldSendExpectedModel()
        {   //check if the right values are sent and the correct model
            //Arrange
            Tour filledModel;
            _viewModel.IsNewTour = true;
            _viewModel.Tourname = "Test";
            _viewModel.TransportType = TourPlanner.Model.TransportType.fastest;
            _viewModel.Description = "Description";
            _viewModel.Start = "Start";
            _viewModel.Destination = "Destiantion";
            _viewModel.CloseAction = new Action(() => Debug.Print("Dummy Text")); //just to get rid of exception
            Tour targetModel = new Tour(_viewModel.Tourname, _viewModel.Start, _viewModel.Destination, _viewModel.Description, _viewModel.TransportType);
            var isSame = false;
            //Act
            _viewModel.OnSubmitClicked += (s, param) =>
            {
                filledModel = param.TourData;            
                foreach (PropertyInfo prop in filledModel.GetType().GetProperties())
                {
                    if (prop.Name == nameof(Tour.Tourname) || prop.Name == nameof(Tour.Start) || prop.Name == nameof(Tour.Destination) || prop.Name == nameof(Tour.RouteInformation) || prop.Name == nameof(Tour.TransportType))
                    {
                        isSame = prop.GetValue(filledModel, null).Equals(prop.GetValue(targetModel, null));
                        if (!isSame)
                            break;
                    }
                }
            };
            _viewModel.Submit.Execute(null);
            //Assert
            Assert.IsTrue(isSame, "Created Model does not match target Model");
        }

        [Test]
        public void SubWindowViewTour_ModifyTour_shouldSendExpectedModel()
        {   //check if the right values are sent and the correct model
            //Arrange
            Tour filledModel;
            _viewModel.IsNewTour = false;
            _viewModel.Tourname = "Test";
            _viewModel.Description = "Description";
            _viewModel.CloseAction = new Action(() => Debug.Print("Dummy Text")); //just to get rid of exception
            Tour targetModel = new Tour(_viewModel.Tourname, "", "", _viewModel.Description, TransportType.bicycle);
            var isSame = false;
            //Act
            _viewModel.OnSubmitClicked += (s, param) =>
            {
                filledModel = param.TourData;
                foreach (PropertyInfo prop in filledModel.GetType().GetProperties())
                {
                    if (prop.Name == nameof(Tour.Tourname) || prop.Name == nameof(Tour.RouteInformation))
                    {
                        isSame = prop.GetValue(filledModel, null).Equals(prop.GetValue(targetModel, null));
                        if (!isSame)
                            break;
                    }
                }
            };
            _viewModel.Submit.Execute(null);
            //Assert
            Assert.IsTrue(isSame, "Created Model does not match target Model");
        }

    }
}