using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Logging;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.Files
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
            return true;
        }
    }
}
