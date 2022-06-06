using System.Collections.Generic;
using DI.Queries;

namespace Di.Qry.Core
{
    public interface IQryRequest
    {
        string Schema { get; }
        long? EntityId { get; }
        string SearchStr { get; }
        IQryFilter Filter { get; }
        PageInfo PageInfo { get; }
        List<SortInfo> SortInfos { get; }
        bool CanSearch();
    }
}