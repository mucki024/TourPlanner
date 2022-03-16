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
                if(_tourname != value)
                {
                    _tourname = value;
                    OnPropertyChanged(nameof(Tourname));
                }
            }
        }
        private ITourFactory _tourfactory; // needs to be removed
        public event EventHandler<string> OnSubmitClicked;
        public RelayCommand Submit { get; }

        public SubWindowViewTour()
        {
            _tourfactory = TourFactory.GetInstance();
            this.Submit = new RelayCommand((_) =>
            {
                Debug.Print("Button clicked");
                this.OnSubmitClicked?.Invoke(this, Tourname);
            });

            //TODO: this should be in main, but the onSUbmit event does not work through two windows!
            //after adding, List needs to be refreshed
            this.OnSubmitClicked += (_, tourName) =>
            {
                // call the BIZ-layer
                
                Debug.Print("adding new tour");
                _tourfactory.addNewTour(tourName);
            };
        }
    }
}
