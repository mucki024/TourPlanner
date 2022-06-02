using System.Collections.Generic;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    public interface ITourFactoryFileAction
    {
        string checkImage(string path);
        public string getDefaultPicture();
        string getImagePath(int TourID);
        Task<bool> exportFile(Tour tour, string path);
        Task<Tour> importFile(string path);
        Task<bool> exportReport(Tour tour, string path);
        Task<bool> exportMultiReport(IEnumerable<Tour> tourModels, string path);

    }
}
