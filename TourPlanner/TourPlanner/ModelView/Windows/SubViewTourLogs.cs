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
                if (SelectedTourLog == null)
                    return;
                TourLog tmp = new TourLog(SelectedTourLog.TourLogID,SelectedTourLog.TourID,SelectedTourLog.Comment,(int)SelectedTourLog.Difficulty,SelectedTourLog.Timestamp,SelectedTourLog.TotalTime,SelectedTourLog.Rating);
                _tourfactory.deleteTourLog(tmp);
                TourLogData.Remove(SelectedTourLog);
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
                EditableTourLogModel tmp = new EditableTourLogModel(item.TourLogID,item.TourID,item.Comment, (int)item.Difficulty,item.Timestamp.ToLocalTime(),item.TotalTime,item.Rating);
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

        /*
         * ERROR Validation
         * 
         */
        /*
        public string Error => string.Empty;
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case nameof(SelectedTourLog.TotalTime): // property name
                    validationMessage = ValidateTime();
                    break;
                case nameof(SelectedTourLog.Rating): // property name
                    validationMessage = ValidateRating();
                    break;
            }
            return validationMessage;
        }

        private string ValidateRating()
        {
            if (SelectedTourLog.Rating > 10)
                return "no ratings greater than 10";
            return string.Empty;
        }

        private string ValidateTime()
        {
            TimeSpan outTime;
            if (!TimeSpan.TryParse(SelectedTourLog.TotalTime.ToString(), out outTime))
            {
                return "Wrong format (hh:mm:ss)!";
            }

            if (SelectedTourLog.TotalTime.ToString() == "00:00:00")
            {
                return "TimeInput is missing";
            }

            return string.Empty;
        }
        */
    }
}
