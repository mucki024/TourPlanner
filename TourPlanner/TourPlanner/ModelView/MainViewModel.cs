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

namespace TourPlanner
{
    public class MainViewModel : ViewModelBase 
    {
        /*
        Textsearch possible, but no return possibility => no input = reset or add clear button
        Adding Tourdata should be possible with a new window
        Binding,Business Layer and Logic needs to be added for Tour Logs
        Subviews required, but where ??
        */

        //variable for other views
        private readonly SubWindowViewTour _subWindowTour;


        private ITourFactory _tourfactory;
        private MouseButtonEventHandler _changebaleTour;
        private Tour _selectedTour;
        private string _searchTour;
        private string _searchText = "Search";
        public ObservableCollection<Tour> TourData { get; } = new ObservableCollection<Tour>();
        public RelayCommand ExecuteTextSearch { get { return new RelayCommand(DisplayTourResults); } }
        public RelayCommand AddTour { get; }
        public RelayCommand DeleteTour { get; }
        public RelayCommand ChangeTour { get; }

        public MouseButtonEventHandler ChangebaleTour
        {
            get => _changebaleTour;
            set
            {   //set selected Tour and update UI
                _changebaleTour = value;
                OnPropertyChanged(nameof(ChangebaleTour));
            }
        }

        public Tour SelectedTour
        {
            get => _selectedTour;
            set
            {   //set selected Tour and update UI
                if (_selectedTour == value)
                    Debug.Print("Clicked twice");
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour));
            }
        }

        public string SearchTour
        {
            get { return _searchTour; }
            set
            {   //value gets updated for every user input character
                if (_searchTour != value)
                {
                    _searchTour = value;
                    OnPropertyChanged(nameof(SearchTour));
                }
            }
        }

        public string SearchText
        {
            get { return _searchText;  }
            set
            {
                if(_searchText == "Search")
                {
                    _searchText = "Clear";
                    OnPropertyChanged(nameof(SearchText));
                }
                else
                {
                    _searchText = "Search";
                    OnPropertyChanged(nameof(SearchText));
                }
            }
        }

        public MainViewModel(SubWindowViewTour subWindowAddTour)
        {
            _tourfactory = TourFactory.GetInstance();

            //init subwindow 
            _subWindowTour = subWindowAddTour;

            subWindowAddTour.OnSubmitClicked += (_, tourName) =>
            {
                // call the BIZ-layer
                Debug.Print("adding new tour");
                _tourfactory.addNewTour(tourName);
            };

            //fill observable collection
            FillToursToCollection();

            AddTour = new RelayCommand((_) =>
            {
                TourWindow window = new TourWindow();
                window.ShowDialog();
                //MessageBox.Show("Your request will be processed.");
                //TourData.Add(new Tour("DummyTour"));
            });

            DeleteTour = new RelayCommand((_) =>
            {
                if (SelectedTour != null)
                    TourData.Remove(SelectedTour);
            });

            ChangeTour = new RelayCommand((_) =>
            {

            });
        }

        private void DisplayTourResults(object searchOrClear)
        {
            bool search = (bool)searchOrClear;
            if (search)     //search command
            {
                IEnumerable foundItems = _tourfactory.searchTour(SearchTour);
                TourData.Clear();
                foreach (Tour item in foundItems)
                {
                    TourData.Add(item);
                }
                SearchText = "";
            }
            else        //clear command
            {
                FillToursToCollection();
                SearchText = "";
                SearchTour = "";
            }
        }

        private void FillToursToCollection()
        {
            foreach (Tour item in _tourfactory.getAllTours())
            {
                TourData.Add(item);
            }
        }
    }
}
