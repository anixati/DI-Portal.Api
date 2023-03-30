using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using DI.Forms.Core;
using DI.Forms.Requests;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class DialogActionHandler : ActionHandlerBase<IDialogHandler>,
        IRequestHandler<DialogActionRequest, DialogActionResponse>
    {
        public DialogActionHandler(IEnumerable<IDialogHandler> handlers, ILoggerFactory loggerFactory)
            : base(handlers, loggerFactory)
        {
        }

        public async Task<DialogActionResponse> Handle(DialogActionRequest request, CancellationToken cancellationToken)
        {
            var handler = GetHandler(request.SchemaKey);
            var result = await handler.Execute(request, request.EntityId);
            return result;
        }
    }
}