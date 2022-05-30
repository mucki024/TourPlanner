using iText.IO.Font.Constants;
using iText.Kernel.Font;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TourPlanner.DataAccess.DAO;
using TourPlanner.Model;

namespace TourPlanner.DataAccess.FileHandling
{
    public class ReportHandlerDAO : IFileHandlerDAO
    {
        public async Task<bool> FileExport(Tour tourModel, string path)
        {
            if (path==null || tourModel == null)
                return false;
            await Task.Run(() =>
            {
                string fileName = path + "\\"+tourModel.TourID+"-"+tourModel.Tourname+".pdf";
                PdfWriter writer = new PdfWriter(fileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph TourNameHeader = new Paragraph(tourModel.Tourname)
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                            .SetFontSize(16)
                            .SetBold();
                Paragraph TourData = new Paragraph("From: "+tourModel.Start+"\n"+"To: "+tourModel.Destination+"\nSummary: " + tourModel.RouteInformation)
                               .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                List Loglist = new List()
                      .SetSymbolIndent(12)
                      .SetListSymbol("")
                      .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
                foreach(TourLog log in tourModel.LogList)
                {
                    Loglist.Add(new ListItem(log.Rating + " " + log.Timestamp))
                        .Add(new ListItem(log.Comment))
                        .Add(new ListItem(log.Difficulty + " " + log.TotalTime))
                        .Add(new ListItem("______________________"));

                }

                document.Add(TourNameHeader);
                document.Add(TourData);
                document.Add(Loglist);
                document.Close();
            });
            return true;
        }

        public Task<Tour> FileImport(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path) 
        {//should get called after full db querry to make sure all tours are supplied
            if (path == null)
                return false;
            await Task.Run(() =>
            {
                string fileName = path + "\\TourReport.pdf";
                PdfWriter writer = new PdfWriter(fileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph Header = new Paragraph("Available Tours")
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                            .SetFontSize(16)
                            .SetBold();
                List Tourlist = new List()
                      .SetSymbolIndent(12)
                      .SetListSymbol(" ")
                      .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
                foreach (Tour tour in tourModels)
                {
                    Tourlist.Add(new ListItem(tour.TourID + ": " + tour.Tourname))
                            .Add(new ListItem(tour.Start + " - " + tour.Destination))
                            .Add(new ListItem(tour.RouteInformation))
                            .Add(new ListItem("________________________"));

                }
                document.Add(Tourlist);
                document.Close();
            });
            return true;
        }
    }
}
