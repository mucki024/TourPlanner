using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.Model
{
    public enum DiffucltyLevel
    {
        easy,medium,hard
    }
    public class TourLog
    {
        public int LogID { get; set; } //changed from string to int
        public string Comment { get; set; }
        public DiffucltyLevel Difficulty  { get; set; }
        public DateTime Timestamp { get; set; }
        public string DateOnly { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }
        public TourLog(int id,string comment,int difficulty,DateTime timestamp,TimeSpan totalTime,int rating)
        { //basic constructor for the TourLogDAO
            this.LogID = id;
            this.Comment = comment;
            this.Difficulty = (DiffucltyLevel)difficulty;
            this.Timestamp = timestamp;
            this.TotalTime = totalTime;
            this.Rating = rating;
            ConvertDate();
        }

        public void ConvertDate()
        {
            DateOnly = Timestamp.ToString("MMMM dd, yyyy");
        }
    }
    
}
