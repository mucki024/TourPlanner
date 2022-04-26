using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.BusinessLayer;
using TourPlanner.Model;

namespace TourPlanner
{
    public class SubViewTourLogs : ViewModelBase
    {
        public ObservableCollection<TourLog> TourLogData { get; } = new ObservableCollection<TourLog>();
        private ITourFactory _tourfactory;
        private int _tourID = -1;

        public RelayCommand AddLog { get; }
        public RelayCommand DeleteLog { get; }

        public SubViewTourLogs(SubWindowViewLog viewModel ,WindowFactory winFac)
        {
            _tourfactory = TourFactory.GetInstance();   //should be done with DI perhaps
            AddLog = new RelayCommand((_) =>
            {
                winFac.CreateLogWindow(_tourID);
            });
            FillLogsToCollection();
            viewModel.OnSubmitClicked += (_, TourClass) =>
            {
                _tourfactory.addNewLog(TourClass);
                FillLogsToCollection();
            };
        }
        private void FillLogsToCollection()
        {
            TourLogData.Clear();
            if (_tourID == -1)
                return;
            foreach (TourLog item in _tourfactory.getAllLogs(_tourID))
            {
                TourLogData.Add(item);
            }
        }

        public void getCurrentSelectedTour(Tour tour)
        {
            this._tourID = tour.TourID;
            FillLogsToCollection();
        }

    }
}
