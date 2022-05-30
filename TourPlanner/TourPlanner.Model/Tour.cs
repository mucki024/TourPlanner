using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public enum TransportType
    {
        AUTO, WALKING, BICYCLE
    }
    public enum ChildFriendliness
    {
        ChildFriendly, OnlyForAdults, notCalculated
    }
    public class Tour
    {
        public int TourID { get; set; } 
        public string Tourname { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public TransportType TransportType { get; set; }
        public double TourDistance { get; set; }
        public TimeSpan EstimatedTime { get; set; }
        public string RouteInformation { get; set; }
        public string ImagePath { get; set; }
        public int Popularity { get; set; }
        public ChildFriendliness ChildFriendliness { get; set; }

        public List<TourLog> LogList { get; set; } = new List<TourLog>(); //changed to log list

        //for import/ export
        public Tour()
        {

        }

        /*
        //for import/ export
        public Tour(int tourId, string tourname, string start, string destination, int transportType, double tourDistance, TimeSpan estimatedTime,string  routeInformation,string imagePath,int popularity,int childFriendliness, List<TourLog> logList)
        {
            this.TourID = tourId;
            this.Tourname = tourname;
            this.Start = start;
            this.Destination = destination;
            this.TransportType = (TransportType)transportType;
            this.TourDistance = tourDistance;
            this.EstimatedTime = estimatedTime;
            this.RouteInformation = routeInformation;
            this.ImagePath = imagePath;
            this.Popularity = popularity;
            this.ChildFriendliness = (ChildFriendliness)childFriendliness;
            this.LogList = logList;
        }
        */
        public Tour(string tourname, string start, string destination, string description, TransportType transportType)
        {
            TourID = 1;
            Tourname = tourname;
            Start = start;
            Destination = destination;
            RouteInformation = description;
            TransportType = transportType;
            TourDistance = 0;
            EstimatedTime = TimeSpan.Zero;
        }
        
        public Tour(int tourId,string tourname, string description, string from, string to, int transportType,double distance, TimeSpan EstimatedTime)
        {
            this.TourID = tourId;
            this.Tourname = tourname;
            this.RouteInformation = description;
            this.Destination = to;
            this.Start = from;
            this.TransportType = (TransportType)transportType;
            this.EstimatedTime = EstimatedTime;
            this.TourDistance = distance;

        }
        
        public void AddLogs(IEnumerable<TourLog> Logs)
        {
            foreach (TourLog log in Logs)
            {
                this.LogList.Add(log);
            }
        }

    }
}
