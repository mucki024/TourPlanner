using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactory : ITourFactoryTour, ITourFactoryLog, ITourFactoryFileAction
    {
    }
}
