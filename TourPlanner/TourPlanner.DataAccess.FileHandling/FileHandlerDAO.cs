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
                string jsonModel = Newtonsoft.Json.JsonConvert.SerializeObject(tourModel);
                //await JsonSerializer.SerializeAsync(createStream, tourModel);
                await File.WriteAllTextAsync(fileName, jsonModel);
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
            catch (Exception e)
            {
                logger.Error($"DAL: Export: something went wrong: {e.Message}");
                return false;
            }
        }

        public async Task<Tour> FileImport(string path)
        {
            try
            {
                using (StreamReader reader = new StreamReader(path))
                {
                    string json = await reader.ReadToEndAsync();
                    //Tour items = JsonSerializer.Deserialize<Tour>(json);
                    Tour items = Newtonsoft.Json.JsonConvert.DeserializeObject<Tour>(json);
                    reader.Dispose();
                    return items;
                }

            }
            catch (FileNotFoundException)
            {
                logger.Error($"DAL: Import: File was not found for {path}");
                return null;
            }
            catch (JsonException)
            {
                logger.Error($"DAL: Import: Deserialize went wrong");
                return null;
            }catch (Exception e)
            {
                logger.Error($"DAL: Import: something went wrong: {e.Message}");
                return null;
            }
        }
        public string CheckFilePath(string path)
        {
            if (File.Exists(path))
            {
                return path;
            }
            return DefaultPicture();
        }

        public string DefaultPicture()
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"images\\white.png";
        }
        public string GetImagePath(int TourID)
        {
            return System.AppDomain.CurrentDomain.BaseDirectory + $"images\\{TourID}.jpeg";
        }

        public void DeletePicture(string path)
        {
            try
            {
                if (!File.Exists(path))
                {
                    logger.Error($"DAL: File: Picture does not exist for path {path} ");
                    return;
                }

                System.GC.Collect();
                System.GC.WaitForPendingFinalizers();
                File.Delete(path);
                logger.Debug("DAL: File: Deleted Picture");
            }
            catch(IOException e)
            {
                logger.Error($"DAL: File: Could not locate File : {e.Message} ");
            }
            catch (Exception e)
            {
                logger.Error($"DAL: File: Could not delete Picture on \"Server\": {e.Message}");
            }
        }

        public Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path)
        {
            throw new NotImplementedException();
        }

    }
}
