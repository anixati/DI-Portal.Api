using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using DI.Forms.Core;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public abstract class ActionHandlerBase<T> : ServiceBase where T:class,IActionHandler
    {
        private static readonly ConcurrentDictionary<string, Lazy<T>> _actions = new();
        protected ActionHandlerBase(IEnumerable<T> handlers, ILoggerFactory logFactory) : base(logFactory)
        {
            foreach (var fb in handlers)
                _actions[$"{fb.SchemaKey.ToLower().Trim()}"] = new Lazy<T>(() => fb);
        }
        protected T GetHandler(string key)
        {
            if (string.IsNullOrEmpty(key))
                throw new Exception("form handler key is required");
            if (!_actions.TryGetValue(key.Trim().ToLower(), out var state))
                throw new Exception($"form handler {key} not configured!");
            return state.Value;
        }
    }
}