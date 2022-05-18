using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public interface IQryProvider
    {
        IEnumerable<IQrySchema> GetSchemaList();
        List<SchemaName> GetSchemas();
        SchemaDef GetSchemaDef(string schemaKey);
        T GetQryState<T>(string qryStateKey) where T : class, IQryState;
        List<QryOption> GetQryOptions(string refDataKey);
    }
}