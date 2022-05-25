using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public class FormSchemaHandler : ServiceBase, IRequestHandler<FormSchemaRequest, FormSchemaResponse>
    {
        private readonly IFormProvider _provider;

        public FormSchemaHandler(IFormProvider provider, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
        }

        public async Task<FormSchemaResponse> Handle(FormSchemaRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return new FormSchemaResponse
            {
                Schema = _provider.GetSchema(request.Name)
            };
        }
    }
}