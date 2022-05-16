using System.Collections.Generic;

namespace Di.Qry.Core
{
    public interface IQryState
    {
        Dictionary<string, IQryField> GetQryFields();
        IPagedContext Compile(IQryFilter filter, PageInfo pageInfo);
        IQryContext Compile();
    }
}