using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class FormSchemaHandler : ActionHandlerBase, IRequestHandler<FormSchemaRequest, FormSchemaResponse>
    {
        private readonly IFormProvider _provider;

        public FormSchemaHandler(IFormProvider provider, IEnumerable<IFormActionHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
            _provider = provider;

        }

        public async Task<FormSchemaResponse> Handle(FormSchemaRequest request, CancellationToken cancellationToken)
        {
            var response = new FormSchemaResponse();
            var handler = GetHandler(request.Name);
            if (request.Type == ActionType.Create)//create 
            {

                response.Schema = _provider.GetSchema($"create_{request.Name}");
                await handler.LoadOptions(response.Schema);
                var rx = await handler.LoadCreateData(response.Schema, request.EntityId.GetValueOrDefault());
                response.InitialValues = rx.InitialValues;
            }
            else if (request.Type == ActionType.Manage)//manage 
            {

                response.Schema = _provider.GetSchema($"manage_{request.Name}");
                await handler.LoadOptions(response.Schema);
                var rx = await handler.LoadCreateData(response.Schema, request.EntityId.GetValueOrDefault());
                response.InitialValues = rx.InitialValues;
            }
            else //view
            {
                response.Schema = _provider.GetSchema($"view_{request.Name}");
                await handler.LoadOptions(response.Schema);
                var rx = await handler.LoadViewData(response.Schema, request.EntityId.GetValueOrDefault());
                response.Entity = rx.Entity;
                response.InitialValues = rx.InitialValues;
                response.HdrValues = rx.HdrValues;
            }
            return response;
        }
    }
}