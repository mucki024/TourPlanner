using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        Task<string> addNewTour(Tour tourModel);
        bool addNewLog(TourLog tourLog);
        IEnumerable<Tour> getAllTours();
        IEnumerable<TourLog> getAllLogs(int tourId);
        IEnumerable<Tour> searchTour(string searchterm);
        void modifyTour(Tour tourModel);
        void modifyTourLog(TourLog tourModel);
        void deleteTour(Tour tourModel);
        void deleteTourLog(TourLog tourModel);
        string checkImage(string path);
        ChildFriendliness calcChildFriendliness(List<TourLog> tourLogList, double dist);
        int calcPopularity(int logCount);
    }
}
