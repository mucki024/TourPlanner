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
            string fileName = path + "\\TourExport.json";

            try
            {
                using FileStream createStream = System.IO.File.Create(fileName);
                await JsonSerializer.SerializeAsync(createStream, tourModel);
                await createStream.DisposeAsync();
                return true;
            }catch (UnauthorizedAccessException)
            {
                logger.Error("DAL: Export: Folder Path Access was not allowed for exporting");
                return false;
            }
            catch (ArgumentException)
            {
                logger.Error("DAL: Export: No model provided for export");
                return false;
            }
            /*
            catch (Exception)
            {
                logger.Error("DAL: Export: Some error occoured");
                return false;
            }
            */
            

            //Tour? weatherForecast = JsonSerializer.Deserialize<Tour>(jsonModel);
        }

        public async Task<Tour> FileImport(string path)
        {
            using (StreamReader reader = new StreamReader("C:\\Users\\MartinMuck\\Desktop\\TourExport.json"))
            {
                string json = await reader.ReadToEndAsync();
                Tour items = JsonSerializer.Deserialize<Tour>(json);
                return items;
            }
        }

        public Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path)
        {
            throw new NotImplementedException();
        }
    }
}
