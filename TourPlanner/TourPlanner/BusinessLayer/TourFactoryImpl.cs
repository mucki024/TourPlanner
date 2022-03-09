using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        public IEnumerable<Tour> getAllTours()
        {
            return new List<Tour>()
            {
                new Tour("DummyTour1"),
                new Tour("DummyTour2"),
                new Tour("DummyTour3"),
            };
        }

        public IEnumerable<Tour> searchTour(string searchterm)
        {
            throw new NotImplementedException();
        }
    }
}
