using System.Collections.Generic;

namespace DI.Reports
{
    public interface IReportProvider
    {
        Report GetReport(int reportId);
        List<Report> GetReports();
    }



    public class ReportConfig
    {
        public ReportConfig()
        {

        }
        public string ReportServer { get; set; }
        public List<Report> Reports { get; set; }
    }

    public class Report
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
        public string Options { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
    }
}