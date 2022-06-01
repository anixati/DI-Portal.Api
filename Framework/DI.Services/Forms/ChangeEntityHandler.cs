using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public class ChangeEntityHandler : ActionHandlerBase, IRequestHandler<EntityTypeRequest, EntityTypeResponse>
    {

        public ChangeEntityHandler(IEnumerable<IFormActionHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
        }

        public async Task<EntityTypeResponse> Handle(EntityTypeRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            var handler = GetHandler(request.SchemaKey);
            var result =  handler.GetEntityType(request);
            return result;
        }
    }
}