using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    static public class TourFactory
    {
        private static ITourFactory _instance;
        public static ITourFactory GetInstance()
        {
            if (_instance == null)
                _instance = new TourFactoryImpl();
            return _instance;
        }
    }
}
