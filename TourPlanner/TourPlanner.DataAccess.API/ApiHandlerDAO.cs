using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess.Common;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.API
{
    public class ApiHandlerDAO : IApiAccessDAO
    {
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
            _routeModel = await routeProc.ReadData<RouteModel>();
            tourModel.TourDistance = _routeModel.Route.Distance;
            tourModel.EstimatedTime = _routeModel.Route.FormattedTime;
            return tourModel;
        }
        public async Task DownloadImage(int tourID)
        {
            MapProcessor imageAccess = new MapProcessor(_connectionApi, tourID);
            imageAccess.PrepareUrl(_routeModel);
            await imageAccess.ReadData();
        }
    }
}
