﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using System.Reflection;
using TourPlanner.DataAccess.Common;

namespace TourPlanner.DataAccess.API
{
    public class RouteProcessor: IRouteAccess
    {
        private string _baseUrlDirection = "http://www.mapquestapi.com/directions/v2/route";
        private string _apiUrl="";
        private string _apiKey = "";

        public RouteProcessor(string apiKey)
        {
            _apiKey = apiKey;
        }
        public void PrepareUrl(object model)
        {
            Tour tourModel = model as Tour;
            if (tourModel == null)
            {
                //logging possible
                return;
            }

            _apiUrl = _baseUrlDirection + $"?key={_apiKey}&from={tourModel.Start}&to={tourModel.Destination}";
        }
        public async Task<T> ReadData<T>()
        {
            using (HttpResponseMessage resp = await ApiHelper.GetInstance().GetAsync(_apiUrl))
            {
                if (resp.IsSuccessStatusCode)
                {
                    if (typeof(T) == typeof(RouteModel))
                    {
                        T tmpModel = await resp.Content.ReadAsAsync<T>();
                        
                        return tmpModel;
                    }
                    return default(T);
                }
                throw new Exception(resp.ReasonPhrase);
            }
        }
    }
}
