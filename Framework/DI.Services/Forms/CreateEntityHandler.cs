using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public class CreateEntityHandler : ActionHandlerBase, IRequestHandler<FormActionRequest, FormActionResult>
    {

        public CreateEntityHandler(IEnumerable<IFormActionHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
        }

        public async Task<FormActionResult> Handle(FormActionRequest request, CancellationToken cancellationToken)
        {
            var handler = GetHandler(request.SchemaKey);
            var result = await handler.CreateEntity(request.Data, request.EntityId);
            return result;
        }
    }
}