using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public class Tour
    {
        public string TourID { get; set; }
        public string Tourname { get; set; }
        public List<string> LogIDs { get; set; } = new List<string>();

        public Tour(string tourname)
        {
            TourID = Guid.NewGuid().ToString();
            Tourname = tourname;
        }

    }
}
