using System.Data;
using System.Threading.Tasks;

namespace Di.Qry.Core
{
    public interface IQryConnFactory
    {
        Task<IDbConnection> CreateConnection();
    }
}