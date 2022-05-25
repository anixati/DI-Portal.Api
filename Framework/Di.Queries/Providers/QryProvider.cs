using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI;
using Di.Qry.Core;
using Di.Qry.Schema;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Providers
{
    public class QryProvider : QryService,IQryProvider
    {
        private static readonly ConcurrentDictionary<string, Lazy<IQryState>> QryStates = new();
        private readonly IQryDataSource _dataSource;
        private readonly IEnumerable<IQrySchema> _schemas;
        public QryProvider(IEnumerable<IQrySchema> schemas, IQryDataSource dataSource, ILoggerFactory logFactory) : base(logFactory)
        {
            _schemas = schemas;
            _dataSource = dataSource;
            foreach (var schema in _schemas)
            {
                QryStates[$"{schema.SchemaName.ToLower().Trim()}"] = new Lazy<IQryState>(() => schema.Create());
            }
        }
        public SchemaDef GetSchemaDef(string schemaKey)
        {
            schemaKey.ThrowIfEmpty($"schema name cannot be empty");
            var qs = GetQryState(schemaKey);
            qs.ThrowIfNull();
            return new SchemaDef
            {
                Title = qs.Title,
                Columns = qs.GetQryColumns()
            };
        }
        public T GetQryState<T>(string qryStateKey) where T : class, IQryState
        {
            return (T)GetQryState(qryStateKey);
        }
        private IQryState GetQryState(string qryStateKey)
        {
            if (string.IsNullOrEmpty(qryStateKey))
                throw new Exception("Schema key is required");
            if (!QryStates.TryGetValue(qryStateKey.Trim().ToLower(), out var qstate))
                throw new Exception($"Schema {qryStateKey} not configured!");
            return qstate.Value;
        }
        public IEnumerable<IQrySchema> GetSchemaList()
        {
            return _schemas?.Where(x => x.SchemaType != SchemaType.RefDataQuery)
                       .Select(x => x).ToList() ??
                   Enumerable.Empty<IQrySchema>();
        }
        public List<SchemaName> GetSchemas()
        {
            return GetSchemaList().Select(x=> new SchemaName(x.SchemaName)).ToList();
        }
        public List<QryOption> GetQryOptions(string refDataKey)
        {
            var qState = GetQryState<QryState>(refDataKey);
            var qContext = qState.Compile();
            return _dataSource
                .GetList<QryOption>(qContext).Result;
        }
    }
}