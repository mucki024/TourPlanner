using NUnit.Framework;
using TourPlanner;
using Moq;
using System;
using System.Diagnostics;
using TourPlanner.Model;
using System.Reflection;

namespace TourPlannerTest
{
    public class SubWindowViewLogTest
    {
        private SubWindowViewLog _viewModel;
        [SetUp]
        public void Setup()
        {
            _viewModel = new SubWindowViewLog();
        }

        [Test]
        public void SubWindowViewLog_shouldSendExpectedModel()
        {   //check if the right values are sent 
            //Arrange
            TourLog filledModel;
            _viewModel.TourID = 1;
            _viewModel.Comment = "comment";
            _viewModel.Difficulty = DiffucltyLevel.easy;
            _viewModel.Timestamp = DateTime.Now;
            _viewModel.TotalTime = "00:00:20";
            _viewModel.Rating = 0;
            _viewModel.CloseAction = new Action(() => Debug.Print("Dummy Text")); //just to get rid of exception
            TourLog targetModel = new TourLog(0, _viewModel.TourID, _viewModel.Comment, (int)_viewModel.Difficulty, _viewModel.Timestamp.ToUniversalTime(), TimeSpan.Parse(_viewModel.TotalTime), _viewModel.Rating);
            var isSame = false;
            //Act
            _viewModel.OnSubmitClicked += (s, param) =>
            {
                filledModel = param;            
                foreach (PropertyInfo prop in filledModel.GetType().GetProperties())
                {
                    if (prop.Name == nameof(TourLog.TourID) || prop.Name == nameof(TourLog.Comment) || prop.Name == nameof(TourLog.Difficulty) || prop.Name == nameof(TourLog.Timestamp) || prop.Name == nameof(TourLog.TotalTime) || prop.Name == nameof(TourLog.Rating))
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