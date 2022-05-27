using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Logging;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.API
{
    public class ApiHandlerDAO : IApiAccessDAO
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();
        private string _connectionApi = "";
        private RouteModel _routeModel;
        public ApiHandlerDAO(string data)
        {
            _connectionApi = data;
            //IRouteAccess routeAcces = DALFactory.GetRouteApi();
        }
        public async Task<Tour> GetRouteInfo(Tour tourModel)
        {
            RouteProcessor routeProc = new RouteProcessor(_connectionApi);
            routeProc.PrepareUrl(tourModel);
            try
            {
                _routeModel = await routeProc.ReadData<RouteModel>();
                logger.Debug("DAL:Retrieved Distance/Time from API");
            }
            catch (SystemException)
            {
                logger.Error("DAL:Could not retrieve Route from API");
                return null;
            }

            if (_routeModel.Route.FormattedTime.Hours > 23 || _routeModel.Route.FormattedTime.Days > 0)//check if time not to big for timespan from postgres
            {
                logger.Error("DAL:Route is to large to save in DB");
                return null;
            }
            if ( _routeModel.Route.Distance < 1)
            {
                logger.Error("DAL:No Route found, but no error from API");
                return null;
            }
            tourModel.TourDistance = _routeModel.Route.Distance;
            tourModel.EstimatedTime = _routeModel.Route.FormattedTime;
            return tourModel;
        }
        public async Task<bool> DownloadImage(int tourID)
        {
            MapProcessor imageAccess = new MapProcessor(_connectionApi, tourID);
            imageAccess.PrepareUrl(_routeModel);
            try
            {
                await imageAccess.ReadData();
                logger.Debug("DAL:Retrieved Image from API and saved it");
                return true;
            }
            catch (System.IO.DirectoryNotFoundException)
            {
                logger.Error("DAL:The file for the Map could not be created");
                
            }
            catch (SystemException)
            {
                logger.Error("DAL:Could not load the Image from API");
            }
            return false;
        }
    }
}
