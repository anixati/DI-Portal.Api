using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Providers;
using Di.Qry.Requests;
using Di.Qry.Schema;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Queries
{
    public class QueryHandler : ServiceBase, IRequestHandler<QryRequest, QryResponse>
    {
        private readonly IQryProvider _provider;
        private readonly IQryDataSource _dataSource;
        public QueryHandler(IQryProvider provider, IQryDataSource handler, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
            _dataSource = handler;
        }
        public async Task<QryResponse> Handle(QryRequest request, CancellationToken cancellationToken)
        {
            var schema = _provider.GetSchemaList()
                .FirstOrDefault(x => string.Compare(x.SchemaName, request.SchemaName, StringComparison.OrdinalIgnoreCase) == 0);

            if (schema == null)
                throw new Exception($"Unknown schema requested !");

            switch (schema.SchemaType)
            {
                case SchemaType.DataQuery:
                    return await RetrieveSqlData(request);
                default:
                    throw new Exception("Schema type not handled");
            }

        }

        private async Task<QryResponse> RetrieveSqlData(QryRequest request)
        {
            var queryState = _provider.GetQryState<QryState>(request.SchemaName);
            if (queryState == null)
                throw new Exception("Unable to get query state");

            var response = new QryResponse(request.PageInfo);
            var pagedContext = queryState.Compile(request.Filter, request.PageInfo);
            var dbResult = await _dataSource.ExecuteQuery(pagedContext);
            if (dbResult != null && queryState.HasSubQueries)
                foreach (var sQry in queryState.SubQueries())
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