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
using Microsoft.WindowsAPICodePack.Dialogs;

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

        public RelayCommand ExportFile { get; }
        public RelayCommand ImportFile { get; }
        public RelayCommand Report { get; }
        public RelayCommand Multireport { get; }

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
        public MainViewModel(IWindowFactory TourWindow, SubWindowViewTour vmTourWindow, SubViewTourDescription vmTourDescpr, SubViewTourLogs vmTourLogs, SubWindowViewLog vmLogWindow)
        {
            logger.Debug("Pres: Started Application");
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
                {
                    _tourfactory.deleteTour(SelectedTour);
                    TourData.Remove(SelectedTour);
                }
            });

            ChangeTour = new RelayCommand((_) =>
            {
                if (SelectedTour != null)
                {
                    var tmpTourWindow = TourWindow as WindowFactory;
                    tmpTourWindow.CreateNewWindow(_selectedTour);
                }
            });

            vmLogWindow.OnNewLogAdded += (_, fullTourData) =>
            {
                FillToursToCollection();
            };

            ExportFile = new  RelayCommand(async (_) =>
            {
                if(SelectedTour == null)
                {
                    MessageBox.Show("please select a tour!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                string tmpPath = RetrieveFolderPath();
                if(!await _tourfactory.exportFile(SelectedTour, tmpPath))
                {
                    MessageBox.Show("export failed, try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Export succeeded", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });

            ImportFile = new RelayCommand(async (_) =>
            {
                string tmpPath = RetrieveFilePath();
                Tour? tmpTour;
                if ((tmpTour = await _tourfactory.importFile(tmpPath)) == null)
                {
                    MessageBox.Show("import failed, try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                int tmpID;
                if ((tmpID = await _tourfactory.addNewTour(tmpTour)) <= 0) //if false=> something wrong with api
                {
                    if(tmpID == -1)
                        MessageBox.Show("Unable to dowload/save Image", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if(tmpID == -2)
                        MessageBox.Show("Unable to process Route, please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    if (tmpID == -3)
                        MessageBox.Show("Route is too long, please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);

                    return;
                }
                foreach (var log in tmpTour.LogList)
                {
                    log.TourID = tmpID;
                    log.Timestamp = log.Timestamp.ToUniversalTime();
                    _tourfactory.addNewLog(log);
                }
                FillToursToCollection();
            });

            Report = new RelayCommand(async (_) =>
            {
                if (SelectedTour == null)
                {
                    MessageBox.Show("please select a tour!", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                string tmpPath = RetrieveFolderPath();
                if (!await _tourfactory.exportReport(SelectedTour, tmpPath))
                {
                    MessageBox.Show("report failed, try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Generated Tour Report", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            }); 
            
            Multireport = new RelayCommand(async (_) =>
            {
                string tmpPath = RetrieveFolderPath();
                if (!await _tourfactory.exportMultiReport(_tourfactory.getAllTours(), tmpPath))
                {
                    MessageBox.Show("report failed, try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                MessageBox.Show("Generated Multi Report", "Information", MessageBoxButton.OK, MessageBoxImage.Information);
            });

        }

        private string RetrieveFolderPath() //returns users selected folder
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.IsFolderPicker = true;
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        private string RetrieveFilePath() //returns users selected folder
        {
            CommonOpenFileDialog dialog = new CommonOpenFileDialog();
            dialog.InitialDirectory = "C:\\Users";
            dialog.Filters.Add(new CommonFileDialogFilter("JSON File", "*.json"));
            if (dialog.ShowDialog() == CommonFileDialogResult.Ok)
            {
                return dialog.FileName;
            }
            return string.Empty;
        }

        private void IntSubWindowForTours(SubWindowViewTour viewModelTour)
        {
            _subWindowTour = viewModelTour;

            viewModelTour.OnSubmitClicked += async (_, fullTourData) =>
            {
                // call the BIZ-layer
                if (fullTourData.IsCreateTour)
                {
                    int tmp;
                    if ((tmp = await _tourfactory.addNewTour(fullTourData.TourData)) <= 0) //if false=> something wrong with api
                    {
                        if (tmp == -1)
                            MessageBox.Show("Unable to dowload/save Image", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (tmp == -2)
                            MessageBox.Show("Unable to process Route, please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                        if (tmp == -3)
                            MessageBox.Show("Route is too long, please try again", "Save error", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }
                if (!fullTourData.IsCreateTour)
                    _tourfactory.modifyTour(fullTourData.TourData);
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