using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Reports;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace DI.Services.Reports
{
    public class ReportProvider: ServiceBase,IReportProvider
    {
        private ReportConfig _reportConfig;
        public ReportProvider(ILoggerFactory logFactory, IOptionsMonitor<ReportConfig> reportOptionsMonitor) : base(logFactory)
        {
            reportOptionsMonitor.OnChange(config => {
                _reportConfig = config;
            });
            _reportConfig = reportOptionsMonitor.CurrentValue;

        }
        public Report GetReport(int reportId)
        {
            _reportConfig.ThrowIfNull($"Failed to get report options");
            var serverUrl = _reportConfig.ReportServer;
            var reports = _reportConfig.Reports;
            var report = reports.FirstOrDefault(x => x.Id == reportId);
            if (report == null) throw new FileNotFoundException($"configuration not found for report id:{reportId}");
            report.Url = $"{serverUrl}/{report.Url}?rs:embed=true";
            return report;
        }

        public List<Report> GetReports()
        {
            _reportConfig.ThrowIfNull($"Failed to get report options");
            var serverUrl = _reportConfig.ReportServer;
            var reports = _reportConfig.Reports;
            if (reports == null || reports.Count == 0) throw new Exception($"No reports configured");
            return reports.Select(x => new Report
            {
                Id = x.Id,
                Title = x.Title,
                Description = x.Description,
                Url = $"{serverUrl}/{x.Url}?rs:embed=true"

            }).ToList();
        }
    }
}
