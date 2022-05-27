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
    public class EditableTourLogModel : TourLog, IEditableObject
    {
        public event EventHandler<TourLog> OnChangeOfTourLog;

        public void BeginEdit()
        {
            //Debug.Print("beginning edit");
        }

        public void CancelEdit()
        {
        }

        public void EndEdit()
        {
            OnChangeOfTourLog?.Invoke(this, new TourLog(TourLogID,TourID,Comment,(int)Difficulty, Timestamp.ToUniversalTime(), TotalTime,Rating));
        }

        public EditableTourLogModel(int id,int fid, string comment, int difficulty, DateTime timestamp, TimeSpan totalTime, int rating) : base(id, fid,  comment,  difficulty,  timestamp,  totalTime,  rating)
        {
            this.TourLogID = id;
            this.TourID = fid;
            this.Comment = comment;
            this.Difficulty = (DiffucltyLevel)difficulty;
            this.Timestamp = timestamp;
            this.TotalTime = totalTime;
            this.Rating = rating;
            ConvertDate();
        }
    }
}
