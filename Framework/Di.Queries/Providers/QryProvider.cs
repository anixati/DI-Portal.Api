using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Di.Qry.Contracts;
using Di.Qry.Core;
using Di.Qry.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Providers
{
    public class QryProvider : IQryProvider
    {
        private static readonly ConcurrentDictionary<string, Lazy<IQryState>> _states = new();
        private readonly IEnumerable<IQrySchema> _schemas;
        private readonly IQryDbProvider _handler;
        private readonly ILogger _logger;

        public QryProvider(IEnumerable<IQrySchema> schemas, IQryDbProvider handler, ILoggerFactory logFactory) 
        {
            _logger = logFactory.CreateLogger(this.GetType().Name); ;
            _schemas = schemas;
            _handler = handler;
            foreach (var schema in _schemas)
                _states[$"{schema.SchemaName.ToLower().Trim()}"] 
                    = new Lazy<IQryState>(() => schema.Create());
        }

        public IQryState GetState(string qryStateKey)
        {
            if (string.IsNullOrEmpty(qryStateKey))
                throw new Exception($"Schema key is required");
            if (!_states.TryGetValue(qryStateKey.Trim().ToLower(), out var state))
                throw new Exception($"Schema {qryStateKey} not configured!");
            return state.Value;
        }

        #region Query Configuration

        public JObject GetConfig(string schemaKey)
        {
            
            var qs = GetState(schemaKey);
            var fds = qs.GetFields();
            var def = new JObject();
            foreach (var qf in fds.Values)
            {
                qf.Operators = GetOperators(qf);
                if (qf.IsMetaData)
                    qf.Options = GetQryOptions(qf.RefDataId);
                def[qf.QueryKey] = qf.GetConfig();
            }
            var queryConfig = new JObject
            {
                ["fields"] = def
            };
            return queryConfig;
        }

        private string[] GetOperators(Field qf)
        {
            switch (qf.FieldType)
            {
                case FieldType.Text:
                    return new string[] { "=", "like" };

                case FieldType.Numeric:
                    return new string[] { "=", "!=", "<", ">" };

                case FieldType.Date:
                    return new string[] { "=", "<", "<=", ">", "=>" };

                case FieldType.Bool:
                    return new string[] { "=", "!=" };
            }

            return null;
        }



        private List<QryOption> GetQryOptions(string key)
        {
            var qState = GetState(key);
            var qContext = qState.Compile();
            return _handler.GetDbRefData(qContext).Result;
        }

        #endregion
        public IEnumerable<string> GetSchemaList()
        {
            return _schemas?.Where(x => x.RefData == false).Select(x => x.SchemaName).ToList() ?? Enumerable.Empty<string>();
        }

        public JObject GetSchemas()
        {
            return new JObject { { "schemas", JToken.FromObject(GetSchemaList().ToArray()) } };
        }
    }
}