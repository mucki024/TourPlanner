using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.DAO
{
    public interface ITourLogDAO
    {
        TourLog GetById(int tourLogId);
        TourLog AddNewTourLog(int tourId, string comment, int diffuclty, DateTime timestamp, TimeSpan totalTime,int rating);
        IEnumerable<TourLog> GetLogsForTour(int tourId);
        TourLog UpdateTourLog(TourLog model);
        bool DeleteTourLog(TourLog model);
    }
}
