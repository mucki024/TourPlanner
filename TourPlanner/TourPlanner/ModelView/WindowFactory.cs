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
        public WindowFactory(SubWindowViewTour viewModel)
        {
            _viewModel = viewModel;
        }

        public void CreateNewWindow()
        {
            TourWindow window = new TourWindow();
            window.DataContext = _viewModel;
            _viewModel.Tourname = "";
            _viewModel.Description = "";
            _viewModel.Start = "";
            _viewModel.Destination = "";
            _viewModel.TransportType = "";
            //if (_viewModel.CloseAction == null)  //property to close window
            _viewModel.CloseAction = new Action(() => window.Close());
            window.ShowDialog();
        }

        public void CreateNewWindow(Tour tourAttributes)
        {
            TourWindow window = new TourWindow();
            window.DataContext = _viewModel;
            _viewModel.Tourname = tourAttributes.Tourname;
            _viewModel.Description = tourAttributes.RouteInformation;
            _viewModel.Start = tourAttributes.Start;
            _viewModel.Destination = tourAttributes.Destination;
            _viewModel.TransportType = tourAttributes.TransportType;
            if (_viewModel.CloseAction == null)  //property to close window
                _viewModel.CloseAction = new Action(() => window.Close());
            window.ShowDialog();
        }
    }
}
