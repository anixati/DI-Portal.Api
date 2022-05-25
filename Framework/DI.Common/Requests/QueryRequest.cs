using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DI.Requests
{

    public class SortVal
    {
        public string Id { get; set; }
        public bool Desc { get; set; }
    }

    public class QueryRequest : SearchRequestBase
    {

        public string SearchStr { get; set; }
        public List<SortVal> SortBy { get; set; } = new List<SortVal>();
    }
}