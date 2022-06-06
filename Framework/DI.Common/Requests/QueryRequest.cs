using System.Collections.Generic;
using DI.Queries;

namespace DI.Requests
{
    public class QueryRequest : SearchRequestBase
    {
        public long? EntityId { get; set; }
        public IQryFilter Filter { get; set; }
        public string SearchStr { get; set; }
        public List<SortVal> SortBy { get; set; } = new();
    }
}