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
using System.IO;

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
            //IEnumerable<Tour> tours = tourDAO.GetTours(); //specific function is only for ID PK so we need to do it like that            
            return tourDAO.SearchForTours(searchterm);
        }

        public async Task addNewTour(Tour tourModel) // in the business layer we need to translate the data input to a Tour
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            IApiAccessDAO accessDao = DALFactory.GetApi();
            tourModel = await accessDao.GetRouteInfo(tourModel);
            Tour tmpTour = tourDAO.AddNewTour(tourModel);

            await accessDao.DownloadImage(tmpTour.TourID);
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

        public void modifyTour(Tour tourModel)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            tourDAO.UpdateTour(tourModel);
        }
        public void modifyTourLog(TourLog tourModel)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            tourLogDAO.UpdateTourLog(tourModel);
        }
        public void deleteTour(Tour tourModel)
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            tourDAO.DeleteTour(tourModel);
        }
        public void deleteTourLog(TourLog tourModel)
        {
            ITourLogDAO tourLogDAO = DALFactory.CreateTourLogDAO();
            tourLogDAO.DeleteTourLog(tourModel);
        }

        public string checkImage(string path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            return System.AppDomain.CurrentDomain.BaseDirectory + $"images\\white.png";
        }
    }
}
