using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class CreateEntityHandler : ActionHandlerBase<IFormActionHandler>, IRequestHandler<FormActionRequest, FormActionResult>
    {

        public CreateEntityHandler(IEnumerable<IFormActionHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
        }

        public async Task<FormActionResult> Handle(FormActionRequest request, CancellationToken cancellationToken)
        {
            var handler = GetHandler(request.SchemaKey);
            if (request.Type == ActionType.Manage)
                return await ManageEntity(request, handler);
            return await CreateEntity(request, handler);
        }

        private static async Task<FormActionResult> ManageEntity(FormActionRequest request, IFormActionHandler handler)
        {
            var result = await handler.ManageEntity(request.Data, request.EntityId.GetValueOrDefault());
            return result;
        }
        private static async Task<FormActionResult> CreateEntity(FormActionRequest request, IFormActionHandler handler)
        {
            var result = await handler.CreateEntity(request.Data, request.EntityId);
            return result;
        }
    }

}