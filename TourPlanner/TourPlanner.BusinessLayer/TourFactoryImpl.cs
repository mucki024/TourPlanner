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

        public async Task<string> addNewTour(Tour tourModel) // in the business layer we need to translate the data input to a Tour
        {
            ITourDAO tourDAO = DALFactory.CreateTourDAO();
            IApiAccessDAO accessDao = DALFactory.GetApi();
            tourModel = await accessDao.GetRouteInfo(tourModel);
            if(tourModel != null)
            {
                Tour tmpTour = tourDAO.AddNewTour(tourModel);
                if (!await accessDao.DownloadImage(tmpTour.TourID))
                    return "Unable to dowload/save Image";
                return string.Empty;
            }
            return "Unable to process Route, please try again";
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

        public ChildFriendliness calcChildFriendliness(List<TourLog> tourLogList, double dist)
        {
            return CalculateChildFHelper.CheckChildfriendlíness(tourLogList, dist);
        }
        public int calcPopularity(int logCount)
        {
            return CalculatePopHelper.CheckPopularity(logCount);
        }
        
        public async Task<bool> exportFile(Tour model, string path)
        {
            IFileHandlerDAO fileDAO= DALFactory.GetFileHandler();
            return await fileDAO.FileExport(model, path);
        }

        public async Task<Tour> importFile(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> exportReport(Tour model, string path)
        {
            IFileHandlerDAO fileDAO = DALFactory.GetReportHandler();
            return await fileDAO.FileExport(model, path);
        }

        public async Task<bool> exportMultiReport(IEnumerable<Tour> tourModels, string path)
        {
            IFileHandlerDAO fileDAO = DALFactory.GetReportHandler();
            return await fileDAO.MultiExport(tourModels, path);
        }
    }
}
