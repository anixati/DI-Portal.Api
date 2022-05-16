using Di.Qry.Contracts;
using Di.Qry.Core;

namespace Di.Qry.Schema
{
    public class PagedContext
    {
        public PageInfo PageInfo { get; set; }
        public IQryContext CountQry { get; set; }
        public IQryContext DataQry { get; set; }
    }
}