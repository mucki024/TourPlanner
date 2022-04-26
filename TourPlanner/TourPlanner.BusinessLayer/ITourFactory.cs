using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        bool addNewTour(Tour tourModel);
        bool addNewLog(TourLog tourLog);
        IEnumerable<Tour> getAllTours();
        IEnumerable<TourLog> getAllLogs(int tourId);
        IEnumerable<Tour> searchTour(string searchterm);
    }
}
