using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Logging;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.FileHandling
{
    public class FileHandlerDAO : IFileHandlerDAO
    {
        private static ILoggerWrapper logger = LoggerFactory.GetLogger();

        public FileHandlerDAO()
        {

        }
        public async Task<bool> FileExport(Tour tourModel, string path)
        {
            string jsonModel = JsonSerializer.Serialize(tourModel);

            string fileName = path + "\\TourExport.json";

            using FileStream createStream = System.IO.File.Create(fileName);
            await JsonSerializer.SerializeAsync(createStream, jsonModel);
            await createStream.DisposeAsync();

            //Tour? weatherForecast = JsonSerializer.Deserialize<Tour>(jsonModel);
            return true;
        }

        public Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path)
        {
            throw new NotImplementedException();
        }
    }
}
