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

namespace TourPlanner
{
    public class MainViewModel : ViewModelBase 
    {
        private ITourFactory _tourfactory;

        public ObservableCollection<Tour> TourData { get; } = new ObservableCollection<Tour>();
        private MouseButtonEventHandler _changebaleTour;
        public MouseButtonEventHandler ChangebaleTour
        {
            get => _changebaleTour;
            set
            {   //set selected Tour and update UI
                _changebaleTour = value;
                OnPropertyChanged(nameof(ChangebaleTour));
            }
        }

        private Tour _selectedTour;
        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {   //set selected Tour and update UI
                if (_selectedTour == value)
                    Debug.Print("Clicked twice");
                _selectedTour = value;
                //OnPropertyChanged(nameof(SelectedTour));
            }
        }
        public RelayCommand ExecuteTextSearch { get; }
        public RelayCommand AddTour { get; }
        public RelayCommand DeleteTour { get; }
        public RelayCommand ChangeTour { get; }

        public MainViewModel()
        {
            _tourfactory = TourFactory.GetInstance();
            foreach (Tour item in _tourfactory.getAllTours())
            {
                TourData.Add(item);
            }
            /*
            ExecuteTextSearch = new RelayCommand((_) =>
            {

            });
            */
            AddTour = new RelayCommand((_) =>
            {
                Window window = new Window(); // wrong window 
                window.Height = 400;
                window.Width = 300;
                window.ShowDialog();

                //MessageBox.Show("Your request will be processed.");
                TourData.Add(new Tour("DummyTour"));
            });

            DeleteTour = new RelayCommand((_) =>
            {
                if (SelectedTour != null) 
                    TourData.Remove(SelectedTour);
            });

            ChangeTour = new RelayCommand((_) =>
            {
                //if (SelectedTour != null)
                    
            });

        }

    }
}
