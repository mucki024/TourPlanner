using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.BusinessLayer;

namespace TourPlanner
{
    public class MainViewModel : ViewModelBase 
    {
        private ITourFactory _tourfactory;
        public ObservableCollection<Tour> TourData { get; } = new ObservableCollection<Tour>();
        
        /* TODO: implement selected Tour behaivor
        private bool _selectedTour;
        public bool SelectedTour
        {
            get => _selectedTour;
            set
            {   //set selected Tour and update UI
                _selectedTour = value;
                OnPropertyChanged(nameof(SelectedTour);
            }
        }*/
        public RelayCommand ExecuteTextSearch { get; }
        public RelayCommand AddTour { get; }

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
                TourData.Add(new Tour("DummyTour"));
                //OnPropertyChanged()
            });
            
        }

    }
}
