using System.Collections.Generic;

namespace Di.Qry.Core
{
    public interface IQryRequest
    {
        string SchemaName { get; }
        IQryFilter Filter { get; }
        PageInfo PageInfo { get; }
        List<string> SortInfo { get; }
    }
}