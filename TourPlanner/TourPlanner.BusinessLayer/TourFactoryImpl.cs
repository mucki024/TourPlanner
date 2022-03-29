using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.BusinessLayer
{
    internal class TourFactoryImpl : ITourFactory
    {
        List<Tour> _dummyList = new List<Tour>() { 
            new Tour("DummyTour1"),
            new Tour("DummyTour2"), };
        public IEnumerable<Tour> getAllTours()
        {
            //next step => this should be connected to data access layer 
            return _dummyList;
        }

        public IEnumerable<Tour> searchTour(string searchterm)
        {
            IEnumerable<Tour> tours = getAllTours();
            return tours.Where(x => x.Tourname.Contains(searchterm));
        }

        //this needs to be extended => how should the whole user input be transmitted? => JSON?
        public bool addNewTour(string tourname) // in the business layer we need to translate the data input to a Tour
        {
            _dummyList.Add(new Tour(tourname));
            return true;
        }
    }
}
