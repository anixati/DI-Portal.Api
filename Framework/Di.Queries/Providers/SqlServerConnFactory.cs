using System.Data;
using System.Threading.Tasks;
using Di.Qry.Contracts;
using Microsoft.Data.SqlClient;

namespace Di.Qry.Providers
{
    public class SqlServerConnFactory : IQryConnFactory
    {
        private readonly string _connStr;

        public SqlServerConnFactory(string connStr)
        {
            _connStr = connStr;
        }

        public async Task<IDbConnection> CreateConnection()
        {
            var sqlConn = new SqlConnection(_connStr);
            await sqlConn.OpenAsync();
            return sqlConn;
        }
    }
}