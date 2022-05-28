using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.DAO
{
    public interface IFileHandlerDAO
    {
        public Task<bool> FileExport(Tour tourModel, string path);

    }
}
