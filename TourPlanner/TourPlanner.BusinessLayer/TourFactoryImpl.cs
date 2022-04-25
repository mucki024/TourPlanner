using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> getAllTours()
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            return tourDAO.GetTours();
        }

        public IEnumerable<Tour> searchTour(string searchterm)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            IEnumerable<Tour> tours = tourDAO.GetTours(); //specific function is only for ID PK so we need to do it like that
            return tours.Where(x => x.Tourname.Contains(searchterm));
        }

        public bool addNewTour(Tour tourModel) // in the business layer we need to translate the data input to a Tour
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            tourDAO.AddNewTour(tourModel);
            return true;
        }

        public IEnumerable<TourLog> getAllLogs(int tourId)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.GetLogsForTour(tourId);
        }
    }
}
