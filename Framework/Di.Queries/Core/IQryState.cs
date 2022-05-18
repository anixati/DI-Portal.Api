using System.Collections.Generic;
using Di.Qry.Schema.Types;

namespace Di.Qry.Core
{
    public interface IQryState
    {
        List<GridColumn> GetQryColumns();
        Dictionary<string, IQryField> GetQryFields();
        IPagedContext Compile(IQryRequest request);
        IQryContext Compile();
    }
}