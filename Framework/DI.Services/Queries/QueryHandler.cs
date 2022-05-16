using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Contracts;
using Di.Qry.Providers;
using Di.Qry.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Queries
{
    public class QueryHandler : ServiceBase, IRequestHandler<QryRequest, QryResponse>
    {
        private readonly IQryProvider _provider;
        private readonly IQryDbProvider _dbhandler;
        public QueryHandler(IQryProvider provider, IQryDbProvider dbhandler, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
            _dbhandler = dbhandler;
        }

        public Task<QryResponse> Handle(QryRequest request, CancellationToken cancellationToken)
        {
            var queryState = _provider.GetState(request.SchemaName);
            var pagedContext = queryState.Compile(request.Filter, request.PageInfo);
            return _dbhandler.ExecuteQuery(pagedContext);
        }
    }



}