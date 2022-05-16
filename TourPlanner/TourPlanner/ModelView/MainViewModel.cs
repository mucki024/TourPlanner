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
using System.IO;
using TourPlanner.Logging;

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
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        //variable for other views

        private SubWindowViewTour _subWindowTour;
        private readonly SubViewTourDescription _subViewTourDescr;
        private readonly SubViewTourLogs _subViewTourLogs;

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
                if (_selectedTour != value)
                {
                    _selectedTour = value;
                    if(_selectedTour!= null)
                        DisplayTourDescription();
                }
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
            get { return _searchText; }
            set
            {
                if (_searchText == "Search")
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

        //Todo: add TourFactory through DI
        public MainViewModel(IWindowFactory TourWindow, SubWindowViewTour vmTourWindow, SubViewTourDescription vmTourDescpr, SubViewTourLogs vmTourLogs)
        {
            logger.Debug("Started Application");
            _tourfactory = TourFactory.GetInstance();
            IntSubWindowForTours(vmTourWindow);
            _subViewTourDescr = vmTourDescpr;
            _subViewTourLogs = vmTourLogs;

            //fill observable collection
            FillToursToCollection();

            AddTour = new RelayCommand((_) =>
            {
                TourWindow.CreateNewWindow();
            });

            DeleteTour = new RelayCommand((_) =>
            {
                if (SelectedTour != null)
                    TourData.Remove(SelectedTour);
            });

            //Todo: two Subwindows required => button behaivoiur different => one subview for input 
            //Todo: view logic should not be in Viewmodel? 
            ChangeTour = new RelayCommand((_) =>
            {
                if (SelectedTour != null)
                {
                    var tmpTourWindow = TourWindow as WindowFactory;
                    tmpTourWindow.CreateNewWindow(_selectedTour);
                }
            });
        }

        private void IntSubWindowForTours(SubWindowViewTour viewModelTour)
        {
            _subWindowTour = viewModelTour;

            viewModelTour.OnSubmitClicked += async (_, TourClass) =>
            {
                // call the BIZ-layer
                await _tourfactory.addNewTour(TourClass);
                FillToursToCollection();
            };
        }

        private void DisplayTourResults(object searchOrClear)
        {
            bool search = (bool)searchOrClear;
            if (search)     //search command
            {
                TourData.Clear();
                IEnumerable foundItems = _tourfactory.searchTour(SearchTour);
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
            TourData.Clear();
            foreach (Tour item in _tourfactory.getAllTours())
            {
                TourData.Add(item);
            }
        }
        private void DisplayTourDescription()
        {
            _subViewTourDescr.displayAttributes(_selectedTour);
            _subViewTourLogs.getCurrentSelectedTour(_selectedTour);
        }
    }
}