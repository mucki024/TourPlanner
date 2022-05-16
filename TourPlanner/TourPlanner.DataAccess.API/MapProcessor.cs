using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;
using System.Reflection;
using TourPlanner.DataAccess.Common;
using System.IO;
using System.Diagnostics;
using System.Globalization;

namespace TourPlanner.DataAccess.API
{
    public class MapProcessor : IRouteAccess
    {
        private static string _baseUrlMap= "http://www.mapquestapi.com/staticmap/v5/map";
        private string _apiUrl = "";
        private string _apiKey = "";
        private int _tourId;

        public MapProcessor(string apiKey, int tourid)
        {
            _apiKey = apiKey;
            _tourId = tourid;
        }
        public void PrepareUrl(object model)
        {
            RouteModel routeModel = model as RouteModel;
            if (routeModel == null)
            {
                //logging possible
                return;
            }

            _apiUrl = _baseUrlMap + $"?key={_apiKey}&size=640,480&zoom=11&session={routeModel.Route.SessionId}&defaultMarker=marker&boundingBox={routeModel.Route.BoundingBox.Ul.Lat.ToString("G", CultureInfo.InvariantCulture)},{routeModel.Route.BoundingBox.Ul.Lng.ToString("G", CultureInfo.InvariantCulture)},{routeModel.Route.BoundingBox.Lr.Lat.ToString("G", CultureInfo.InvariantCulture)},{routeModel.Route.BoundingBox.Lr.Lng.ToString("G", CultureInfo.InvariantCulture)}";
        }
        public async Task<T> ReadData<T>()
        {
            var fileInfo = new FileInfo($"images\\{_tourId}.jpeg");
            HttpClient curInst = ApiHelper.GetInstance();
            ApiHelper.ChangeResponseTypeToImage();
            using (var resp = await curInst.GetAsync(_apiUrl))
            {
                
                if (resp.IsSuccessStatusCode)
                {
                    await using var image = await resp.Content.ReadAsStreamAsync();
                    await using var file = File.Create(fileInfo.FullName);
                    image.Seek(0, SeekOrigin.Begin);
                    image.CopyTo(file);
                    Debug.Print(file.Name);
                    return default(T);
                }
                throw new Exception(resp.ReasonPhrase);
            }
        }
    }
}
