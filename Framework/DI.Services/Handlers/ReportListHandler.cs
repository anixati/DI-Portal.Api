using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Reports;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class ReportListHandler : ServiceBase, IRequestHandler<ReportListRequest, ReportListResponse>
    {
        private readonly IReportProvider _provider;

        public ReportListHandler(IReportProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<ReportListResponse> Handle(ReportListRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            if (!request.Id.HasValue)
                return new ReportListResponse
                {
                    Reports = _provider.GetReports()
                };

            var rp = _provider.GetReport(request.Id.GetValueOrDefault());
            rp.ThrowIfNull("No report found for given id ");
            return new ReportListResponse
            {
                Reports = new List<Report> {rp}
            };
        }
    }
}