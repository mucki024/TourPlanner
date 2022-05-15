using System;
using System.Net.Http;

namespace TourPlanner.DataAccess.API
{
    public static class ApiHelper
    {
        private static HttpClient _ClientApi = null;
        public static HttpClient GetInstance()
        {
            if (_ClientApi == null)
            {
                _ClientApi = new HttpClient();
                _ClientApi.DefaultRequestHeaders.Accept.Clear();
                _ClientApi.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            }
            return _ClientApi;
        }


    }
}
