using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        Task addNewTour(Tour tourModel);
        bool addNewLog(TourLog tourLog);
        IEnumerable<Tour> getAllTours();
        IEnumerable<TourLog> getAllLogs(int tourId);
        IEnumerable<Tour> searchTour(string searchterm);
        string checkImage(string path);
    }
}
