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
using TourPlanner.BusinessLayer;

namespace TourPlanner
{
    public class SubViewTourDescription : ViewModelBase
    {
        public ITourFactory _fac;
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

        private TransportType _transportType;
        public TransportType TransportType
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

        private int _popularity;
        public int Popularity
        {
            get { return _popularity; }
            set
            {
                if (_popularity != value)
                {
                    _popularity = value;
                    OnPropertyChanged(nameof(Popularity));
                }
            }
        }

        private ChildFriendliness _childFriendl;
        public ChildFriendliness ChildFriendl
        {
            get { return _childFriendl; }
            set
            {
                if (_childFriendl != value)
                {
                    _childFriendl = value;
                    OnPropertyChanged(nameof(ChildFriendl));
                }
            }
        }


        private string _imageName;
        public string ImageName
        {
            get { return _imageName; }
            set
            {
                if (_imageName != value)
                {
                    _imageName = _fac.checkImage(value); //return fallback image => only to fix xaml binding exceptions
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
            this.ImageName = _fac.getImagePath(tour.TourID);
            calculateAdditionalData(tour);
        }

        private void calculateAdditionalData(Tour tour)
        {
            this.Popularity = _fac.calcPopularity(tour.LogList.Count());
            this.ChildFriendl = _fac.calcChildFriendliness(tour.LogList, tour.TourDistance);
        }

        public RelayCommand MyCommand { get; set; }

        public SubViewTourDescription()
        {
            _fac = TourFactory.GetInstance();
            _imageName = _fac.getDefaultPicture();
        }
    }
}
