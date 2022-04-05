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
    public class SubWindowViewTour : ViewModelBase
    {
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

        private string _transportType;
        public string TransportType
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

        public event EventHandler<Tour> OnSubmitClicked;
        public RelayCommand Submit { get; }
        public Action CloseAction { get; set; }

        public SubWindowViewTour()
        {
            //ResetBindings();
            this.Submit = new RelayCommand((_) =>
            {
                Tour tmpTour = new Tour(Tourname, Start, Destination, Description, TransportType);
                OnSubmitClicked?.Invoke(this, tmpTour);
                CloseAction();
            });
        }
    }
}
