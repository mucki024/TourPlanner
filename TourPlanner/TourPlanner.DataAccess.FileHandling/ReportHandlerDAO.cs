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
            await Task.Run(() =>
            {
                string fileName = path + "\\"+tourModel.TourID+"-"+tourModel.Tourname+".pdf";
                PdfWriter writer = new PdfWriter(fileName);
                PdfDocument pdf = new PdfDocument(writer);
                Document document = new Document(pdf);
                Paragraph TourNameHeader = new Paragraph(tourModel.Tourname)
                            .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA))
                            .SetFontSize(14)
                            .SetBold();
                Paragraph TourData = new Paragraph("From: "+tourModel.Start+"\n"+"To: "+tourModel.Destination+"\n" + tourModel.RouteInformation)
                               .SetFont(PdfFontFactory.CreateFont(StandardFonts.HELVETICA));
                List Loglist = new List()
                      .SetSymbolIndent(12)
                      .SetListSymbol("\u2022")
                      .SetFont(PdfFontFactory.CreateFont(StandardFonts.TIMES_BOLD));
                foreach(TourLog log in tourModel.LogList)
                {
                    Loglist.Add(new ListItem(log.Rating + " " + log.Timestamp))
                        .Add(new ListItem(log.Comment))
                        .Add(new ListItem(log.Difficulty + " " + log.TotalTime));

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

        public Task<bool> MultiExport(IEnumerable<Tour> tourModels, string path)
        {
            throw new NotImplementedException();
        }
    }
}
