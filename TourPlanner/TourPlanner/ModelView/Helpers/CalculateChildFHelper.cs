using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner
{
    public class CalculateChildFHelper
    {
        public static ChildFriendliness CheckChildfriendlíness(List<TourLog> tourLogList, double dist)
        {
            if (tourLogList == null || tourLogList.Count == 0)
                return ChildFriendliness.notCalculated;
            var containsHard = tourLogList.Any(log => log.Difficulty== DiffucltyLevel.hard);
            var tooLongTimeCount = tourLogList.Count(log => log.TotalTime > TimeSpan.FromHours(12));
            if (!containsHard && tooLongTimeCount <= 2 && dist < 9.0)
                return ChildFriendliness.ChildFriendly;
            return ChildFriendliness.OnlyForAdults;
        }
    }
}
