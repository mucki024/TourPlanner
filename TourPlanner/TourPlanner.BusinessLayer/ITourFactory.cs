using System.Collections.Generic;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
        bool addNewTour(string tourname);
        IEnumerable<Tour> getAllTours();
        IEnumerable<Tour> searchTour(string searchterm);
    }
}
