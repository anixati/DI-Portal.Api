using System.Collections.Generic;
using Di.Qry.Core;
using Di.Qry.Schema;

namespace Di.Qry.Contracts
{
    public interface IQryState
    {
        PagedContext Compile(IQryFilter filter, PageInfo pageInfo);
        IQryContext Compile();
        IQryState OrderBy(string column, bool desc = false);
        IQryState Page(int limit, int offset);
        IQryState Where(string column, object value);
        Dictionary<string, Field> GetFields();
    }
}