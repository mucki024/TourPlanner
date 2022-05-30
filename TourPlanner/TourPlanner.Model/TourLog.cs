﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        public int TourLogID { get; set; }
        public int TourID { get; set; } 
        public string Comment { get; set; }
        public DiffucltyLevel Difficulty  { get; set; }
        public DateTime Timestamp { get; set; }
        public string DateOnly { get; set; }
        public TimeSpan TotalTime { get; set; }
        public int Rating { get; set; }

        //for import / export
        public TourLog()
        {

        }

        public TourLog(int id,int fid,string comment,int difficulty,DateTime timestamp,TimeSpan totalTime,int rating)
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

        /*
        //import/ export
        public TourLog(int tourLogID, int tourID, string comment, int difficulty, DateTime timestamp,string dateOnly, TimeSpan totalTime, int rating)
        {
            this.TourLogID = tourLogID;
            this.TourID = tourID;
            this.Comment = comment;
            this.Difficulty = (DiffucltyLevel)difficulty;
            this.Timestamp = timestamp;
            this.DateOnly = dateOnly;
            this.TotalTime = totalTime;
            this.Rating = rating;
        }
        */

        public void ConvertDate()
        {
            DateOnly = Timestamp.ToString("MMMM dd, yyyy");
        }
    }
    
}
