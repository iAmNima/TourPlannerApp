using System;
using System.Collections.Generic;
using System.Text;
using TourPlannerApp.Models;

namespace TourPlannerApp.BusinessLayer.QuestPdf
{
    public class PdfModel
    {
        public TourEntry TourInfo { get; set; }
        public IEnumerable<LogEntry> LogsInfo { get; set; }
        public byte[] ImageBytes { get; set; }
    }
}
