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
    public class EditableTourLogModel : TourLog, IEditableObject, IDataErrorInfo
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
        
        public EditableTourLogModel()
        {

        }
        
        public string Error => string.Empty;
        public string this[string columnName]
        {
            get
            {
                return Validate(columnName);
            }
        }

        private string Validate(string propertyName)
        {
            // Return error message if there is error on else return empty or null string
            string validationMessage = string.Empty;
            switch (propertyName)
            {
                case nameof(TotalTime): // property name
                    validationMessage = ValidateTime();
                    break;
                case nameof(Rating): // property name
                    validationMessage = ValidateRating();
                    break;

            }
            return validationMessage;
        }

        private string ValidateTime()
        {
            if (TotalTime.Days > 0 || TotalTime.Hours > 23)
                return "Total TIme is too high => max 23:59:59!";

            return string.Empty;
        }
        private string ValidateRating()
        {
            if (Rating > 10)
                return "no ratings greater than 10";
            return string.Empty;
        }

    }
}
