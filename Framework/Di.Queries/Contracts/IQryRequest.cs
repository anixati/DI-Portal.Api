using System.Collections.Generic;
using Di.Qry.Core;

namespace Di.Qry.Contracts
{
    public interface IQryRequest
    {
        string SchemaName { get; }
        IQryFilter Filter { get; }
        PageInfo PageInfo { get; }
        List<string> SortInfo { get; }
    }
}