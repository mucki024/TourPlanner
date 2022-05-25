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
    public struct ViewData //struct to send data via subscriber pattern to mainview
    {
        public ViewData(Tour tour,bool isCreateTour)
        {
            TourData = tour;
            IsCreateTour = isCreateTour;
        }

        public Tour TourData { get; }
        public bool IsCreateTour { get; }
    }
    public class SubWindowViewTour : ViewModelBase
    {
        public  int TourID { get; set; }
        public double TourDistance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        private string _tourname;
        public string Tourname
        {
            get { return _tourname; }
            set
            {
                if (_tourname != value)
                {
                    _tourname = value;
                    OnPropertyChanged(nameof(Tourname));
                }
            }
        }

        private string _description;
        public string Description
        {
            get { return _description; }
            set
            {
                if (_description != value)
                {
                    _description = value;
                    OnPropertyChanged(nameof(Description));
                }
            }
        }

        private string _start;
        public string Start
        {
            get { return _start; }
            set
            {
                if (_start != value)
                {
                    _start = value;
                    OnPropertyChanged(nameof(Start));
                }
            }
        }

        private string _destination;
        public string Destination
        {
            get { return _destination; }
            set
            {
                if (_destination != value)
                {
                    _destination = value;
                    OnPropertyChanged(nameof(Destination));
                }
            }
        }

        private TransportType _transportType;
        public TransportType TransportType
        {
            get { return _transportType; }
            set
            {
                if (_transportType != value)
                {
                    _transportType = value;
                    OnPropertyChanged(nameof(TransportType));
                }
            }
        }

        private bool _isNewTour; //val to check which form it is => modify or create
        public bool IsNewTour
        {
            get { return _isNewTour;  }
            set{ _isNewTour = value; }
        }
        private Visibility _AttributeVisibility; //cut start and end destination 
        public Visibility AttributeVisibility
        {
            get { return _AttributeVisibility; }
            set
            {
                _AttributeVisibility = value;
                OnPropertyChanged(nameof(AttributeVisibility));
            }
        }
        public event EventHandler<ViewData> OnSubmitClicked;
        public RelayCommand Submit { get; }
        public Action CloseAction { get; set; }

        public SubWindowViewTour()
        {
            this.Submit = new RelayCommand((_) =>
            {
                Tour tmpTour;
                if (IsNewTour)
                {
                    tmpTour = new Tour(Tourname, Start, Destination, Description, TransportType);
                }
                else
                {
                    tmpTour = new Tour(TourID, Tourname, Description, Start, Destination, (int)TransportType, TourDistance, EstimatedTime);
                }
                OnSubmitClicked?.Invoke(this, new ViewData(tmpTour,IsNewTour));
                CloseAction();
            });
        }
    }
}
