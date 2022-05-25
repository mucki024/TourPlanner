using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.BusinessLayer;

namespace TourPlanner
{
    public class EditableTourLogModel :IEditableObject
    {
        public event EventHandler<TourLog> OnChangeOfTourLog;
        public int TourID { get; set; } //changed from string to int
        public string Comment { get; set; }
        public DiffucltyLevel Difficulty { get; set; }
        public DateTime Timestamp { get; set; }
        public string DateOnly { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

        public void BeginEdit()
        {
            Debug.Print("beginning edit");
        }

        public void CancelEdit()
        {
        }

        public void EndEdit()
        {
            Debug.Print(this.TourID.ToString());
            Debug.Print(this.Comment.ToString());
            OnChangeOfTourLog?.Invoke(this, new TourLog(TourID,Comment,(int)Difficulty,Timestamp,TotalTime,Rating));
        //TourFactory.GetInstance().modifyTourLog();
        }

        public EditableTourLogModel(int id, string comment, int difficulty, DateTime timestamp, TimeSpan totalTime, int rating)
        { //basic constructor for the TourLogDAO
            this.TourID = id;
            this.Comment = comment;
            this.Difficulty = (DiffucltyLevel)difficulty;
            this.Timestamp = timestamp;
            this.TotalTime = totalTime;
            this.Rating = rating;
        }

    }
}
