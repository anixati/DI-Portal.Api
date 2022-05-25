using System.Threading;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Queries
{
    public class SchemaHandler : ServiceBase, IRequestHandler<SchemaRequest, SchemaResponse>
    {
        private readonly IQryProvider _provider;

        public SchemaHandler(IQryProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<SchemaResponse> Handle(SchemaRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return new SchemaResponse
            {
                Schema = _provider.GetSchemaDef(request.Name)
            };
        }
    }
}