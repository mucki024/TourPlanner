using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory
    {
       
        IEnumerable<Tour> getAllTours();
        IEnumerable<Tour> searchTour(string searchterm);
    }
}
