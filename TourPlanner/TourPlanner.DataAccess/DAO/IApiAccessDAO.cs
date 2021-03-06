using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.DAO
{
    public interface IApiAccessDAO
    {
        public Task<Tour> GetRouteInfo(Tour tourModel);
        public Task DownloadImage(int tourID);

    }
}
