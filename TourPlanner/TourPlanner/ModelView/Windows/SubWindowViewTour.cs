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
        public event EventHandler<string> OnSubmitClicked;
        public RelayCommand Submit { get; }
        public Action CloseAction { get; set; }

        public SubWindowViewTour()
        {
            this.Submit = new RelayCommand((_) =>
            {
                OnSubmitClicked?.Invoke(this, Tourname);
                CloseAction();
            });

            //TODO: this should be in main, but the onSUbmit event does not work through two windows!
            //after adding, List needs to be refreshed
            /*
            this.OnSubmitClicked += (_, tourName) =>
            {
                // call the BIZ-layer
                //((MainWindow)Application.Current.MainWindow)

                //need to invoke main event here
                Debug.Print("adding new tour");
                _tourfactory.addNewTour(tourName);
            };
            */
        }
    }
}
