using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactoryLog
    {
        bool addNewLog(TourLog tourLog);
        IEnumerable<TourLog> getAllLogs(int tourId);
        void modifyTourLog(TourLog tourModel);
        void deleteTourLog(TourLog tourModel);

    }
}
