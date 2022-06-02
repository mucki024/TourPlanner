using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactoryTour
    {
        Task<int> addNewTour(Tour tourModel);
        IEnumerable<Tour> getAllTours();
        IEnumerable<Tour> searchTour(string searchterm);
        void modifyTour(Tour tourModel);
        void deleteTour(Tour tourModel);
        ChildFriendliness calcChildFriendliness(List<TourLog> tourLogList, double dist);
        int calcPopularity(int logCount);

    }
}
