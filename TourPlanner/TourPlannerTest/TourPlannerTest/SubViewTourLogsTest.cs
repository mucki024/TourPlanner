using NUnit.Framework;
using TourPlanner;
using Moq;
using System;
using System.Diagnostics;
using TourPlanner.Model;
using System.Reflection;
using TourPlanner.BusinessLayer;

namespace TourPlannerTest
{
    public class SubViewTourLogsTest
    {
        private SubViewTourLogs _viewModel;
        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SubViewTourLogs_addShouldCreateNewWindow()
        {   //
            //Arrange
            var subWindowLogMock = new Mock<SubWindowViewLog>();
            var windowFactoryMock = new Mock<WindowFactory>();
            var tourFactoryMock = new Mock<ITourFactory>();
            windowFactoryMock.Setup(mock => mock.CreateLogWindow(It.IsAny<int>()));
            _viewModel = new SubViewTourLogs(subWindowLogMock.Object, windowFactoryMock.Object, tourFactoryMock.Object);
            //Act 
            _viewModel.AddLog.Execute(null);
            //Assert
            windowFactoryMock.VerifyAll();
        }

        [Test]
        public void SubViewTourLogs_deleteLogShouldReduceDatagrid()
        {   // Test if ObservableCollection is reduced by one  for Delete
            //Arrange
            var subWindowLogMock = new Mock<SubWindowViewLog>();
            var windowFactoryMock = new Mock<WindowFactory>();
            var tourFactoryMock = new Mock<ITourFactory>();
            _viewModel = new SubViewTourLogs(subWindowLogMock.Object, windowFactoryMock.Object, tourFactoryMock.Object);
            EditableTourLogModel tmp = new EditableTourLogModel(0,1,"comment",(int)DiffucltyLevel.easy,DateTime.Now,TimeSpan.Zero,0);
            _viewModel.SelectedTourLog = tmp;
            _viewModel.TourLogData.Add(tmp);
            int tmpCount = _viewModel.TourLogData.Count;
            //Act 
            _viewModel.DeleteLog.Execute(null);
            //Assert
            Assert.IsTrue((tmpCount- _viewModel.TourLogData.Count) == 1, "Delete Function did not remove element from datagrid");
        }

        [Test]
        public void SubViewTourLogs_deleteLogShouldCallBusinessLayer()
        {   // Test if Business Layer is called for Delete
            //Arrange
            var subWindowLogMock = new Mock<SubWindowViewLog>();
            var windowFactoryMock = new Mock<WindowFactory>();
            var tourFactoryMock = new Mock<ITourFactory>();
            tourFactoryMock.Setup(mock => mock.deleteTourLog(It.IsAny<TourLog>()));
            _viewModel = new SubViewTourLogs(subWindowLogMock.Object, windowFactoryMock.Object, tourFactoryMock.Object);
            _viewModel.SelectedTourLog = new EditableTourLogModel(); 
            //Act 
            _viewModel.DeleteLog.Execute(null);
            //Assert
            tourFactoryMock.VerifyAll();
        }


    }
}