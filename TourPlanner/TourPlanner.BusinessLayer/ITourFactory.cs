using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        bool addNewTour(Tour tourModel);
        IEnumerable<Tour> getAllTours();
        IEnumerable<Tour> searchTour(string searchterm);
    }
}
