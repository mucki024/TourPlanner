using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.BusinessLayer;
using System.Drawing.Printing;
using Aspose.Cells.Drawing;
using System.Windows;
using System.Collections;
using System;

namespace TourPlanner
{
    public class SubWindowViewLog : ViewModelBase
    {
        private string _comment;
        public string Comment {
            get { return _comment; }
            set
            {
                if (_comment != value)
                {
                    _comment = value;
                    OnPropertyChanged(nameof(Comment));
                }
            }
        }
        private DiffucltyLevel _difficulty;
        public DiffucltyLevel Difficulty
        {
            get { return _difficulty; }
            set
            {
                if (_difficulty != value)
                {
                    _difficulty = value;
                    OnPropertyChanged(nameof(Difficulty));
                }
            }
        }

        private DateTime _timestamp;
        public DateTime Timestamp
        {
            get { return _timestamp; }
            set
            {
                if (_timestamp != value)
                {
                    _timestamp = value;
                    OnPropertyChanged(nameof(Timestamp));
                }
            }
        }

        private TimeSpan _totalTime;
        public TimeSpan TotalTime
        {
            get { return _totalTime; }
            set
            {
                if (_totalTime != value)
                {
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }

        private int _rating;
        public int Rating
        {
            get { return _rating; }
            set
            {
                if (_rating != value)
                {
                    _rating = value;
                    OnPropertyChanged(nameof(Rating));
                }
            }
        }

        public int TourID { get; set; }


        public event EventHandler<TourLog> OnSubmitClicked;
        public RelayCommand Submit { get; }
        public Action CloseAction { get; set; }

        public SubWindowViewLog()
        {
            //ResetBindings();
            this.Submit = new RelayCommand((_) =>
            {
                TourLog tmpLog = new TourLog(TourID, Comment,((int)Difficulty), Timestamp, TotalTime, Rating);
                OnSubmitClicked?.Invoke(this, tmpLog);
                CloseAction();
            });
        }
    }
}
