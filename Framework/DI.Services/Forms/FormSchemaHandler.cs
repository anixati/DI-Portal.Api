using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
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

            if (!request.EntityId.HasValue)//create 
            {
                return new FormSchemaResponse
                {
                    Schema = _provider.GetSchema($"create_{request.Name}")
                };
            }
            //view
            var schema = _provider.GetSchema($"view_{request.Name}");
            var handler = GetHandler(request.Name);
            var rx = await handler.LoadViewData(schema, request.EntityId.GetValueOrDefault());
            return new FormSchemaResponse
            {
                Schema = schema,
                Entity = rx.Entity,
                InitialValues = rx.InitialValues
            };
        }
    }
}