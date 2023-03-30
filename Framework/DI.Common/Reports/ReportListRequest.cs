using System.Collections.Generic;
using MediatR;

namespace DI.Reports
{
    public class ReportListRequest : IRequest<ReportListResponse>
    {
        public int? Id { get; set; }
    }

    public class ReportListResponse
    {
        public List<Report> Reports { get; set; }
    }
}