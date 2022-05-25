using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
        public ObservableCollection<EditableTourLogModel> TourLogData { get; } = new ObservableCollection<EditableTourLogModel>();
        private ITourFactory _tourfactory;
        private int _tourID = -1;

        private EditableTourLogModel _selectedTourLog;
        public EditableTourLogModel SelectedTourLog
        {
            get { return _selectedTourLog; }
            set
            {
                if (_selectedTourLog != value)
                {
                    _selectedTourLog = value;
                    OnPropertyChanged(nameof(SelectedTourLog));
                }
            }
        }

        public RelayCommand AddLog { get; }
        public RelayCommand DeleteLog { get; }

        public SubViewTourLogs(SubWindowViewLog viewModel ,WindowFactory winFac)
        {
            _tourfactory = TourFactory.GetInstance();   //should be done with DI perhaps
            AddLog = new RelayCommand((_) =>
            {
                winFac.CreateLogWindow(_tourID);
            });

            DeleteLog = new RelayCommand((_) =>
            {
                //_tourfactory.deleteTourLog(SelectedTourLog);
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
                EditableTourLogModel tmp = new EditableTourLogModel(item.TourID,item.Comment, (int)item.Difficulty,item.Timestamp,item.TotalTime,item.Rating);
                TourLogData.Add(tmp);
                tmp.OnChangeOfTourLog += (_, model) =>
                {
                    _tourfactory.modifyTourLog(model);
                    FillLogsToCollection();
                };
            }
        }

        public void getCurrentSelectedTour(Tour tour)
        {
            this._tourID = tour.TourID;
            FillLogsToCollection();
        }

    }
}
