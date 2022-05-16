using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Di.Qry.Core;
using Di.Qry.Requests;
using Di.Qry.Schema;
using Microsoft.Extensions.Logging;

namespace Di.Qry.Providers
{
    public class SqlServerDataSource : QryService,IQryDataSource
    {

        private readonly IQryConnFactory _connFactory;
        public SqlServerDataSource(IQryConnFactory connFactory, ILoggerFactory factory) : base(factory)
        {
            _connFactory = connFactory;
        }

        public async Task<IHandlerResponse> ExecuteQuery(IPagedContext context)
        {
            Trace($"{context}");
            var rsp = new HandlerResponse(context.DataQry.DataSetName);
            using var dbConn = await _connFactory.CreateConnection();
            var rtn = await dbConn.QueryAsync<int>(context.CountQry.QueryString, context.CountQry.Parameters);
            var count = rtn.First();
            var data = dbConn.Query(context.DataQry.QueryString, context.DataQry.Parameters);
            if (data != null) rsp.SetResult(count, data.Cast<IDictionary<string, object>>());
            return rsp;
        }

        public async Task<List<T>> GetList<T>(IQryContext qContext) where T : class
        {
            using var dbConn = await _connFactory.CreateConnection();
            var refData = await dbConn.QueryAsync<T>(qContext.QueryString, qContext.Parameters);
            return refData.ToList();
        }
        public async Task<IHandlerResponse> ExecuteQuery(IQryContext qContext)
        {

            using var dbConn = await _connFactory.CreateConnection();
            var rsp = new HandlerResponse(qContext.DataSetName);
                var refData = await dbConn.QueryAsync(qContext.QueryString, qContext.Parameters);
                rsp.SetResult(0, refData?.Cast<IDictionary<string, object>>());
                return rsp;
        }

    }
}