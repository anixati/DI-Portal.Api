using System.Data;
using System.Threading.Tasks;

namespace Di.Qry.Contracts
{
    public interface IQryConnFactory
    {
        Task<IDbConnection> CreateConnection();
    }
}