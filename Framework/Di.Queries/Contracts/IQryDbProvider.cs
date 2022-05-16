using System.Collections.Generic;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Requests;
using Di.Qry.Schema;

namespace Di.Qry.Contracts
{
    public interface IQryDbProvider
    {
        Task<List<QryOption>> GetDbRefData(IQryContext qContext);

        Task<QryResponse> ExecuteQuery(PagedContext qContext);
    }
}