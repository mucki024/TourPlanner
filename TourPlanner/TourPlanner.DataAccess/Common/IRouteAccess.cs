using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccess.Common
{
   public interface IRouteAccess
    {
        public void PrepareUrl(object model);
        public Task<T> ReadData<T>();
    }
}
