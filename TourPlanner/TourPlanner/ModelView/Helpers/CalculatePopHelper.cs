using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TourPlanner
{
    public class CalculatePopHelper
    {
        public static int CheckPopularity(int logCount)
        {
            if (logCount == 0)
                return 0;
            if(logCount <= 3)
                return 1;
            if (logCount <= 6)
                return 2;
            if (logCount <= 9)
                return 3;
            if (logCount <= 13)
                return 2;
            return 1;
        }
    }
}
