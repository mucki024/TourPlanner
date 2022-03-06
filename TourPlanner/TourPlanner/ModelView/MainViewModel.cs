using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> TourData { get; } = new ObservableCollection<Tour>();
        public RelayCommand ExecuteTextSearch { get; }
        public RelayCommand AddTour { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {

            ExecuteTextSearch = new RelayCommand((_) =>
            {

            });

            AddTour = new RelayCommand((_) =>
            {
                TourData.Add(new Tour("DummyTour"));
                //OnPropertyChanged()
            });
            
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
