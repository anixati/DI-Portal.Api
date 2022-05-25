using System.Collections.Generic;

namespace Di.Qry.Core
{
    public interface IQryRequest
    {
        string Schema { get; }

        string SearchStr { get; }
        IQryFilter Filter { get; }
        PageInfo PageInfo { get; }
        List<SortInfo> SortInfos { get; }
        bool CanSearch();
    }
}