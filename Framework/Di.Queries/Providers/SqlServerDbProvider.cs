using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Di.Qry.Contracts;
using Di.Qry.Core;
using Di.Qry.Requests;
using Di.Qry.Schema;
using Microsoft.Extensions.Logging;

namespace Di.Qry.Providers
{
    public class SqlServerDbProvider : IQryDbProvider
    {

        private readonly IQryConnFactory _connFactory;
        private readonly ILogger _logger;

        public SqlServerDbProvider(IQryConnFactory connFactory, ILoggerFactory factory)
        {
            _connFactory = connFactory;
            _logger = factory.CreateLogger(this.GetType().Name); ;
        }

        public Task<List<QryOption>> GetDbRefData(IQryContext qContext)
        {
            throw new System.NotImplementedException();
        }

        public async Task<QryResponse> ExecuteQuery(PagedContext context)
        {
            var rsp = new QryResponse(context.PageInfo);
            using var dbConn =  await _connFactory.CreateConnection();
            var rtn = dbConn.Query<int>(context.CountQry.SqlString, context.CountQry.Parameters);
            rsp.PageInfo.Total = rtn.First();
            rsp.Data = dbConn.Query<dynamic>(context.DataQry.SqlString, context.DataQry.Parameters);
            return rsp;
        }
    }
}