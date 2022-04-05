using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour
    {
        public int TourID { get; set; } //why is this string?
        public string Tourname { get; set; }
        public string Start { get; set; }
        public string Destination { get; set; }
        public string TransportType { get; set; }
        public int TourDistance { get; set; }
        public int EstimatedTime { get; set; }
        public string RouteInformation { get; set; }

        public List<TourLog> LogList { get; set; } = new List<TourLog>(); //changed to log list

        public Tour(string tourname)
        {
            TourID = 1;
            Tourname = tourname;
            Start = "A";
            Destination = "B";
            TransportType = "f";
            TourDistance = 0;
            EstimatedTime = 0;
            RouteInformation = "Nope";
        }
        //Objekt irgendwie überarbeiten
        public Tour(int tourId,string tourname, string description, string from, string to, string transportType,int distance, int EstimatedTime)
        {
            this.TourID = tourId;
            this.Tourname = tourname;
            this.RouteInformation = description;
            this.Destination = to;
            this.Start = from;
            this.TransportType = transportType;
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
