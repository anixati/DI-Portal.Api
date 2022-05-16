using System.Collections.Generic;
using Di.Qry.Contracts;
using Di.Qry.Core;
using MediatR;

namespace Di.Qry.Requests
{
    public class QryRequest : IQryRequest, IRequest<QryResponse>
    {
        public string SchemaName { get; set; }

        public IQryFilter Filter { get; set; }

        public PageInfo PageInfo { get; set; } = new PageInfo();

        public List<string> SortInfo { get; set; } = new List<string>();
    }
}