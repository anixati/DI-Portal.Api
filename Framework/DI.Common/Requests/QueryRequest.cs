using System.Collections.Generic;

namespace DI.Requests
{
    public class QueryRequest : SearchRequestBase
    {
        public string SearchStr { get; set; }
        public List<SortVal> SortBy { get; set; } = new();
    }
}