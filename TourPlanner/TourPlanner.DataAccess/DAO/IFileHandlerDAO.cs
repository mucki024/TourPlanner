using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.DAO
{
    public interface IFileHandlerDAO
    {
        public Task<bool> FileExport(Tour tourModel, string path);
        public Task<Tour> FileImport( string path);
        public Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path);
        public string CheckFilePath(string path);
        public string DefaultPicture();
        public string GetImagePath(int TourID);
        public void DeletePicture(string path);
        public List<int> GetToDeleteImages();
        public void MarkToDelete(int tourID);

    }
}
