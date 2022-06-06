using System.Collections.Generic;
using Di.Qry.Core;
using DI.Queries;
using MediatR;

namespace Di.Qry.Requests
{
    public class QryRequest : IQryRequest, IRequest<QryResponse>
    {
        public QryRequest(string schema, long? entityId)
        {
            Schema = schema;
            EntityId = entityId;
        }

        public string Schema { get; }
        public long? EntityId { get; }
        public string SearchStr { get; set; }

        public bool CanSearch()
        {
            return !string.IsNullOrEmpty(SearchStr);
        }

        public IQryFilter Filter { get; set; } = new QryFilter();

        public PageInfo PageInfo { get; set; } = new();
        public List<SortInfo> SortInfos { get; set; } = new();
    }
}