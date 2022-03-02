using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace TourPlanner
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public ObservableCollection<Tour> Data { get; } = new ObservableCollection<Tour>();
        public RelayCommand AddCommand { get; }

        public event PropertyChangedEventHandler PropertyChanged;

        public MainViewModel()
        {
            /*
            AddCommand = new RelayCommand((_) =>
            {
                Data.Add(new HighscoreEntry(this.CurrentUsername, this.CurrentPoints));
                CurrentUsername = string.Empty;
                CurrentPoints = string.Empty;
                OnPropertyChanged(nameof(CurrentUsername));
                OnPropertyChanged("CurrentPoints");
                IsUsernameFocused = true;
            });
            IsUsernameFocused = true;
            */
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            Debug.Print($"propertyChanged \"{propertyName}\"");
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
