using MediatR;
using System.Collections.Generic;

namespace DI.Reports
{
    public class DashboardDataRequest : IRequest<DashboardDataResponse>
    {
        public int DashboardId { get; set; }
    }
    public class DashboardDataResponse
    {
        public List<DashboardItem> Data { get; set; } = new();
    }

    public class DashboardItem
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }
        public string Result { get; set; }
        public string ResultColor { get; set; }
        public string Icon { get; set; }
    }


}