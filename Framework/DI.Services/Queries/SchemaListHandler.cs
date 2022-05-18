using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Queries
{
    public class SchemaListHandler : ServiceBase, IRequestHandler<SchemaListRequest, SchemaListResponse>
    {
        private readonly IQryProvider _provider;

        public SchemaListHandler(IQryProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<SchemaListResponse> Handle(SchemaListRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return new SchemaListResponse()
            {
                Schemas = _provider.GetSchemas()
            };
        }
    }
}