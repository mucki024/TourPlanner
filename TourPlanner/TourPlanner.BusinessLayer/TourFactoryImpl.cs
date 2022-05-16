using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;
using TourPlanner.DataAccess.API;

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

        public async Task addNewTour(Tour tourModel) // in the business layer we need to translate the data input to a Tour
        {
            
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            IRouteAccess routeAcces = DALFactory.GetRouteApi();
            routeAcces.PrepareUrl(tourModel);
            RouteModel model = await routeAcces.ReadData<RouteModel>(); //Todo: needs to be done in DAL

            tourModel.TourDistance = model.Route.Distance;
            tourModel.EstimatedTime = model.Route.FormattedTime;
            Tour tmpTour = tourDAO.AddNewTour(tourModel);
            
            IRouteAccess imageAccess = new MapProcessor("xFA4sS7TC6RZk5gGZSr2vmcljK87l692", tmpTour.TourID);
            imageAccess.PrepareUrl(model);
            await imageAccess.ReadData<RouteModel>();
        }
        public bool addNewLog(TourLog tourLog)
        {
            ITourLogDAO tourLogDao = DALFactory.CreateTourLogDAO();
            tourLogDao.AddNewTourLog(tourLog.TourID,tourLog.Comment,((Int32)tourLog.Difficulty),tourLog.Timestamp,tourLog.TotalTime,tourLog.Rating);
            return true;
        }

        public IEnumerable<TourLog> getAllLogs(int tourId)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            return tourLogDAO.GetLogsForTour(tourId);
        }
    }
}
