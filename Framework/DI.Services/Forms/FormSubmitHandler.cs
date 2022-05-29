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
    public class FormSubmitHandler : ServiceBase, IRequestHandler<FormActionRequest, FormActionResult>
    {
        private static readonly ConcurrentDictionary<string, Lazy<IFormActionHandler>> _actions = new();

        public FormSubmitHandler(IEnumerable<IFormActionHandler> handlers, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            foreach (var fb in handlers)
                _actions[$"{fb.SchemaName.ToLower().Trim()}"] = new Lazy<IFormActionHandler>(() => fb);
        }

        public async Task<FormActionResult> Handle(FormActionRequest request, CancellationToken cancellationToken)
        {
            var handler = GetHandler(request.SchemaKey);
            var result = await handler.Execute(request.Data, request.EntityId);
            return result;
        }

        private IFormActionHandler GetHandler(string qryStateKey)
        {
            if (string.IsNullOrEmpty(qryStateKey))
                throw new Exception("form handler key is required");
            if (!_actions.TryGetValue(qryStateKey.Trim().ToLower(), out var state))
                throw new Exception($"form handler {qryStateKey} not configured!");
            return state.Value;
        }
    }
}