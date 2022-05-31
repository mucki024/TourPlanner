using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner
{
    public class WindowFactory : IWindowFactory
    {
        private SubWindowViewTour _viewModel;
        private SubWindowViewLog _viewModelLog;

        public WindowFactory(SubWindowViewTour viewModel, SubWindowViewLog viewLog)
        {
            _viewModel = viewModel;
            _viewModelLog = viewLog;
        }

        public void CreateNewWindow()
        {
            TourWindow window = new TourWindow();
            window.DataContext = _viewModel;
            _viewModel.TourID = 1;
            _viewModel.Tourname = "";
            _viewModel.Description = "";
            _viewModel.Start = "";
            _viewModel.Destination = "";
            _viewModel.TransportType = TransportType.fastest;
            _viewModel.IsNewTour = true;
            _viewModel.AttributeVisibility = System.Windows.Visibility.Visible;
            //if (_viewModel.CloseAction == null)  //property to close window
            _viewModel.CloseAction = new Action(() => window.Close());
            window.ShowDialog();
        }

        public void CreateLogWindow(int tourID)
        {
            LogWindow window = new LogWindow();
            window.DataContext = _viewModelLog;
            //_viewModelLog.Rating = 10;
            _viewModelLog.TourID = tourID;
            _viewModelLog.CloseAction = new Action(() => window.Close());
            window.ShowDialog();
        }

        //create Window to modify tour data
        public void CreateNewWindow(Tour tourAttributes)
        {
            TourWindow window = new TourWindow();
            window.DataContext = _viewModel;
            _viewModel.TourID = tourAttributes.TourID;
            _viewModel.Tourname = tourAttributes.Tourname;
            _viewModel.Description = tourAttributes.RouteInformation;
            _viewModel.Start = tourAttributes.Start;
            _viewModel.Destination = tourAttributes.Destination;
            _viewModel.TransportType = tourAttributes.TransportType;
            _viewModel.TourDistance = tourAttributes.TourDistance;
            _viewModel.EstimatedTime = tourAttributes.EstimatedTime;
            _viewModel.AttributeVisibility = System.Windows.Visibility.Hidden;
            _viewModel.IsNewTour = false;
            //if (_viewModel.CloseAction == null)  //property to close window
                _viewModel.CloseAction = new Action(() => window.Close());
            window.ShowDialog();
        }
    }
}
