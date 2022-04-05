﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.DAO
{
    public interface ITourDAO
    {
        Tour FindById(int tourId);
        Tour AddNewTour(string tourName,string tourFrom,string tourTo,string tourTransportType,string tourRouteInformation);
        void AddNewTour(Tour tour); //maybe remove if not needed depends on wpf impl.
        IEnumerable<Tour> GetTours();

    }
}
