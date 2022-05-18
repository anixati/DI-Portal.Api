using System.Collections.Generic;
using Di.Qry.Core;
using MediatR;

namespace Di.Qry.Requests
{
    public class QryRequest : IQryRequest, IRequest<QryResponse>
    {
        public QryRequest(string schema)
        {
            Schema = schema;
        }

        public string Schema { get;  }

        public IQryFilter Filter { get; set; } = new QryFilter();

        public PageInfo PageInfo { get; set; } = new PageInfo();
        public List<SortInfo> SortInfos { get; set; } = new List<SortInfo>();
    }
}