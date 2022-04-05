using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner.DataAccess.Common
{
   public interface IImageAccess
    {
        int CreateNewTourImage(string name, string url, DateTime creationTime);
        IEnumerable<FileInfo> SearchForTourImage(int searchId);
    }
}
