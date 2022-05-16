using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using TourPlanner.Model;
using TourPlanner.DataAccess.API;
using TourPlanner.Logging;
using System.Diagnostics;

namespace TourPlanner
{
    public class SubViewTourDescription : ViewModelBase
    {
        private string _title = "Title: ";
        public string Title
        {
            get { return _title; }
            set
            {
                if (_title != value)
                {
                    _title = value;
                    OnPropertyChanged(nameof(Title));
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

        private double _tourDistance;
        public double TourDistance
        {
            get { return _tourDistance; }
            set
            {
                if (_tourDistance != value)
                {
                    _tourDistance = value;
                    OnPropertyChanged(nameof(TourDistance));
                }
            }
        }

        private TimeSpan _estimatedTime;
        public TimeSpan EstimatedTime
        {
            get { return _estimatedTime; }
            set
            {
                if (_estimatedTime != value)
                {
                    _estimatedTime = value;
                    OnPropertyChanged(nameof(EstimatedTime));
                }
            }
        }


        private string _imageName = System.AppDomain.CurrentDomain.BaseDirectory +"\\images\\24.jpeg";
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (_imageName != value)
                {
                    _imageName = value;
                    OnPropertyChanged(nameof(ImageName));
                }
            }
        }
        //is triggered by selected Tour changed
        public void displayAttributes(Tour tour)
        {
            this.Title = tour.Tourname;
            this.Start = tour.Start;
            this.Destination = tour.Destination;
            this.TransportType = tour.TransportType;
            this.TourDistance = tour.TourDistance;
            this.EstimatedTime = tour.EstimatedTime;
            this.Description = tour.RouteInformation;
        }

        public RelayCommand MyCommand { get; set; }


        private async Task LoadAPI()
        {
            ApiHelper.GetInstance();
            //var model = await IApiProcessor.LoadData<RouteModel>("http://www.mapquestapi.com/directions/v2/route?key=xFA4sS7TC6RZk5gGZSr2vmcljK87l692&from=Wien&to=Böheimkirchen");

            //logger.Debug("Hey2");
            //logger.Debug(model.ToString());

        }

        public SubViewTourDescription()
        {
            /*
            MyCommand = new RelayCommand(async (_) =>
            {
                //logger.Debug("Hey1");
                //await LoadAPI();
            });
            */
        }
    }
}
