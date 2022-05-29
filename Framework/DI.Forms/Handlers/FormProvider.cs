using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using DI.Forms.Core;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace DI.Forms.Handlers
{
    public class FormProvider : FormServiceBase, IFormProvider
    {
        private static readonly ConcurrentDictionary<string, Lazy<IFormState>> States = new();
        private readonly IEnumerable<IFormBuilder> _builders;

        public FormProvider(IEnumerable<IFormBuilder> builders, ILoggerFactory logFactory) : base(logFactory)
        {
            _builders = builders;
            foreach (var fb in _builders)
                States[$"{fb.FormName.ToLower().Trim()}"] = new Lazy<IFormState>(() => fb.Create());
        }

        public List<string> GetSchemas()
        {
            return _builders.Select(x => x.FormName.ToLower().TrimEnd()).ToList();
        }

        public FormSchema GetSchema(string schemaKey)
        {
            schemaKey.ThrowIfEmpty("schema name cannot be empty");
            var qs = GetState(schemaKey);
            qs.ThrowIfNull();
            return qs.Build();
        }

        private T GetState<T>(string qryStateKey) where T : class, IFormState
        {
            return (T) GetState(qryStateKey);
        }

        private IFormState GetState(string qryStateKey)
        {
            if (string.IsNullOrEmpty(qryStateKey))
                throw new Exception("Schema key is required");
            if (!States.TryGetValue(qryStateKey.Trim().ToLower(), out var state))
                throw new Exception($"Schema {qryStateKey} not configured!");
            return state.Value;
        }
    }
}