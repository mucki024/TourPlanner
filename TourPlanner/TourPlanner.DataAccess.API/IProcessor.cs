using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccess.API
{
    public interface IProcessor
    {
        public void PrepareUrl(object model);
        public Task<T> ReadData<T>();
    }
}
