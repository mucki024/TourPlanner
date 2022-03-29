using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    enum DiffucltyLevel
    {
        easy,medium,hard
    }
    class TourLog
    {
        public string LogID { get; set; }
        public string Comment { get; set; }
        public DiffucltyLevel Difficulty  { get; set; }
        public DateTime Timestamp { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }
    }
}
