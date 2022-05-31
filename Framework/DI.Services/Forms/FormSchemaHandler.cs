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
    public class FormSchemaHandler : ServiceBase, IRequestHandler<FormSchemaRequest, FormSchemaResponse>
    {
        private readonly IFormProvider _provider;
        private static readonly ConcurrentDictionary<string, Lazy<IFormLoadHandler>> _actions = new();
        public FormSchemaHandler(IFormProvider provider, IEnumerable<IFormLoadHandler> handlers, ILoggerFactory loggerFactory)
            : base(loggerFactory)
        {
            _provider = provider;
            foreach (var fb in handlers)
                _actions[$"{fb.SchemaName.ToLower().Trim()}"] = new Lazy<IFormLoadHandler>(() => fb);
        }
        private IFormLoadHandler GetHandler(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("form handler key is required");
            if (!_actions.TryGetValue(key.Trim().ToLower(), out var state))
                throw new Exception($"form handler {key} not configured!");
            return state.Value;
        }
        public async Task<FormSchemaResponse> Handle(FormSchemaRequest request, CancellationToken cancellationToken)
        {
            var response = new FormSchemaResponse
            {
                Schema = _provider.GetSchema(request.Name)
            };
            if (!request.EntityId.HasValue) return response;
            var handler = GetHandler(request.Name);
            response.InitialValues = await handler.Execute(response.Schema, request.EntityId.GetValueOrDefault());
            return response;
        }
    }
}