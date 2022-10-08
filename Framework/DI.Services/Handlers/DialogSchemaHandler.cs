using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class DialogSchemaHandler : ActionHandlerBase<IDialogHandler>, IRequestHandler<DialogSchemaRequest, DialogSchemaResponse>
    {
        private readonly IFormProvider _provider;

        public DialogSchemaHandler(IFormProvider provider, IEnumerable<IDialogHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
            _provider = provider;

        }

        public async Task<DialogSchemaResponse> Handle(DialogSchemaRequest request, CancellationToken cancellationToken)
        {
            var response = new DialogSchemaResponse();
            var handler = GetHandler(request.Name);
            response.Schema = _provider.GetSchema($"{request.Name}");
            await handler.Initialise(response, request.EntityId);
            return response;
        }
    }
}