﻿using iText.IO.Font.Constants;
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
        public string CheckFilePath(string path)
        {
            throw new NotImplementedException();
        }

        public string DefaultPicture()
        {
            throw new NotImplementedException();
        }

        public void DeletePicture(string path)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> FileExport(Tour tourModel, string path)
        {
            if (path==null || tourModel == null)
                return false;
            Func<ChildFriendliness, string> isChildFriendly = x => x == 0 ? "Yes" : "No";
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
                Paragraph TourData = new Paragraph("From: " +
                    tourModel.Start + "\n" + "To: " + tourModel.Destination + "\nPopularity: " + tourModel.Popularity + "\nSummary: " +
                    tourModel.RouteInformation + "\n" + tourModel.TransportType.ToString() + " Distance: " +
                    tourModel.TourDistance + "\nEstimated Time: " + tourModel.EstimatedTime + " Child friendly: " +
                    isChildFriendly(tourModel.ChildFriendliness))
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

        public string GetImagePath(int TourID)
        {
            throw new NotImplementedException();
        }

        public List<int> GetToDeleteImages()
        {
            throw new NotImplementedException();
        }

        public void MarkToDelete(int tourID)
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
