using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Requests;
using Di.Qry.Schema;
using DI.Security;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Queries
{
    public class QueryHandler : ServiceBase, IRequestHandler<QryRequest, QryResponse>
    {
        private readonly IQryDataSource _dataSource;
        private readonly IQryProvider _provider;
        private readonly ISecurityContext _securityContext;

        public QueryHandler(IQryProvider provider, IQryDataSource handler, ILoggerFactory loggerFactory,
            ISecurityContext securityContext)
            : base(loggerFactory)
        {
            _provider = provider;
            _dataSource = handler;
            _securityContext = securityContext;
        }

        public async Task<QryResponse> Handle(QryRequest request, CancellationToken cancellationToken)
        {
            var schema = _provider.GetSchemaList()
                .FirstOrDefault(x =>
                    string.Compare(x.SchemaName, request.Schema, StringComparison.OrdinalIgnoreCase) == 0);

            if (schema == null)
                throw new Exception("Unknown schema requested !");

            return schema.SchemaType switch
            {
                SchemaType.DataQuery => await RetrieveSqlData(request),
                _ => throw new Exception("Schema type not handled")
            };
        }

        private async Task<QryResponse> RetrieveSqlData(QryRequest request)
        {
            var response = new QryResponse(request.PageInfo);
            var qs = _provider.GetQryState<QryState>(request.Schema);
            if (qs == null)
                throw new Exception("Unable to get query state");


            var pagedContext = await qs.Compile(request, _securityContext);

            Trace(pagedContext.DataQry.QueryString);

            var dbResult = await _dataSource.ExecuteQuery(pagedContext);
            if (dbResult != null && qs.HasSubQueries)
                foreach (var sQry in qs.SubQueries())
                {
                    var keIds = dbResult.Data.Where(x => x.Keys.Any(r => r == sQry.ToKey)).Select(x => x.Values.First())
                        .ToList();
                    if (!keIds.Any()) continue;
                    var qCtx = sQry.Compile(keIds);
                    var rsp = await _dataSource.ExecuteQuery(qCtx);
                    if (rsp.Data == null) continue;
                    var result = rsp.Data
                        .GroupBy(x => x[sQry.ToKey].ToString())
                        .Select(r => new SubType(sQry.SchemaKey, r.Key, r.ToList()));

                    if (!result.Any()) continue;
                    foreach (var item in dbResult.Data)
                    {
                        if (!item.ContainsKey(sQry.ToKey)) continue;
                        var keyValue = item[sQry.ToKey].ToString();
                        var keyItem = result.FirstOrDefault(x => x.Key == keyValue);
                        if (keyItem != null) item[sQry.SchemaKey] = keyItem.Data;
                    }
                }

            response.SetResult(dbResult);
            return response;
        }

        public class SubType
        {
            public SubType(string name, string key, IEnumerable<IDictionary<string, object>> data)
            {
                Name = name;
                Key = key;
                Data = data;
            }

            public string Name { get; }
            public string Key { get; }
            public IEnumerable<IDictionary<string, object>> Data { get; }
        }
    }
}