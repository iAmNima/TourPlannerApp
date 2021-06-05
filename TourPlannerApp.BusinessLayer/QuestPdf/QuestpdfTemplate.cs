using QuestPDF;
using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.BusinessLayer.QuestPdf
{
    public class QuestpdfTemplate : IDocument
    {
        public PdfModel Model { get; }

        public QuestpdfTemplate(PdfModel model)
        {
            Model = model;
        }

        public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

        public void Compose(IContainer container)
        {
            container
            .PaddingHorizontal(50)
            .PaddingVertical(50)
            .Page(page =>
            {
                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().AlignCenter().PageNumber("Page {number}");
            });
        }

        void ComposeHeader(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().Text($"Tour Name: {Model.TourInfo.Name}", TextStyle.Default.Size(20));
                stack.Item().Text($"Tour Distance: {Model.TourInfo.Distance}", TextStyle.Default.Size(10));
                stack.Item().Text($"Tour Description: {Model.TourInfo.Distance}", TextStyle.Default.Size(10));
                stack.Item().PaddingTop(10).Width(300).Height(300).Image(Model.ImageBytes);
            });
        }

        void ComposeContent(IContainer container)
        {
            container.Stack(stack =>
            {
                stack.Item().BorderBottom(1).BorderColor("CCC").Padding(5).Row(row =>
                {
                    row.RelativeColumn().Text("ID", TextStyle.Default.Size(15));
                    row.RelativeColumn().Text("Date", TextStyle.Default.Size(15));
                    row.RelativeColumn().Text("Report", TextStyle.Default.Size(15));
                    row.RelativeColumn().Text("Distance", TextStyle.Default.Size(15));
                    row.RelativeColumn().Text("Rating", TextStyle.Default.Size(15));
                    row.RelativeColumn().Text("Total Time", TextStyle.Default.Size(15));
                });
                foreach(LogEntry log in Model.LogsInfo)
                {
                    stack.Item().BorderBottom(1).BorderColor("CCC").Padding(5).Row(row =>
                    {
                        row.RelativeColumn().Text(log.Id, TextStyle.Default.Size(10));
                        row.RelativeColumn().Text(log.LogDate, TextStyle.Default.Size(10));
                        row.RelativeColumn().Text(log.Report, TextStyle.Default.Size(10));
                        row.RelativeColumn().Text(log.Distance, TextStyle.Default.Size(10));
                        row.RelativeColumn().Text(log.Rating, TextStyle.Default.Size(10));
                        row.RelativeColumn().Text(log.TotalTime, TextStyle.Default.Size(10));
                    });
                }
            });
                
        }
    }
}
