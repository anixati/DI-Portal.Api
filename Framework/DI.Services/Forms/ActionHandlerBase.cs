using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DI.Forms.Core;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public abstract class ActionHandlerBase : ServiceBase
    {
        private static readonly ConcurrentDictionary<string, Lazy<IFormActionHandler>> _actions = new();
        protected ActionHandlerBase(IEnumerable<IFormActionHandler> handlers, ILoggerFactory logFactory) : base(logFactory)
        {
            foreach (var fb in handlers)
                _actions[$"{fb.SchemaKey.ToLower().Trim()}"] = new Lazy<IFormActionHandler>(() => fb);
        }
        protected IFormActionHandler GetHandler(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("form handler key is required");
            if (!_actions.TryGetValue(key.Trim().ToLower(), out var state))
                throw new Exception($"form handler {key} not configured!");
            return state.Value;
        }
    }
}