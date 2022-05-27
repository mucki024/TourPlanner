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
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Globalization;

namespace TourPlanner
{
    public class SubWindowViewLog : ViewModelBase, IDataErrorInfo
    {
        private string _comment ="";
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

        private DateTime _timestamp = DateTime.Today;
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

        private string _totalTime = "00:00:00";
        public string TotalTime
        {
            get { return _totalTime; }
            set
            {
                if (_totalTime != value)
                {
                    SubmitEnable = true;
                    _totalTime = value;
                    OnPropertyChanged(nameof(TotalTime));
                }
            }
        }

        private int _rating = 0;
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

        public bool _submitEnable = false;
        public bool SubmitEnable
        {
            get { return _submitEnable; }
            set
            {
                if (_submitEnable != value)
                {
                    _submitEnable = value;
                    OnPropertyChanged(nameof(SubmitEnable));
                }
            }
        }

        public int TourID { get; set; }

        public event EventHandler<TourLog> OnSubmitClicked;
        public event EventHandler OnNewLogAdded;        //event to update tours for tour popularity
        public RelayCommand Submit { get; }
        public Action CloseAction { get; set; }


        public SubWindowViewLog()
        {
            //ResetBindings();
            this.Submit = new RelayCommand((_) =>
            {
                int TourLogID = 0;
                TourLog tmpLog = new TourLog(TourLogID, TourID, Comment,((int)Difficulty), Timestamp.ToUniversalTime(), TimeSpan.Parse(TotalTime), Rating);
                OnSubmitClicked?.Invoke(this, tmpLog);
                OnNewLogAdded?.Invoke(this, EventArgs.Empty);
                CloseAction();
            });
        }

        /*
         * ERROR Validation
         * 
         */
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
                case nameof(TotalTime): // property name
                    validationMessage = ValidateTime();          
                    break;
                case nameof(Rating): // property name
                    validationMessage = ValidateRating();
                    break;
            }
            if (validationMessage != string.Empty)
                SubmitEnable = false;
            return validationMessage;
        }

        private string ValidateRating()
        {
            if(Rating > 10)
                return "no ratings greater than 10";
            return string.Empty;
        }

        private string ValidateTime()
        {
            TimeSpan outTime;
            if (!TimeSpan.TryParse(TotalTime,out outTime))
            {
                return "Wrong format (hh:mm:ss)!";
            }
            if(outTime.Days > 0 || outTime.Hours > 23)
                return "Total TIme is too high => max 23:59:59!";

            if (TotalTime == "00:00:00")
            {
                return "TimeInput is missing";
            }

            return string.Empty;
        }
    }
}
