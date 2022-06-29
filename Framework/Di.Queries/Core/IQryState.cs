using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Security;

namespace Di.Qry.Core
{
    public interface IQryState
    {
        string Title { get; }
        List<GridColumn> GetQryColumns();
        Dictionary<string, IQryField> GetQryFields();
        Task<IPagedContext> Compile(IQryRequest request,ISecurityContext securityContext);
        IQryContext Compile();
    }
}