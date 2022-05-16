using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public interface IQryProvider
    {
        IEnumerable<IQrySchema> GetSchemaList();
        JObject GetSchemas();
        JObject GetConfig(string schemaKey);
        T GetQryState<T>(string qryStateKey) where T : class, IQryState;
        List<QryOption> GetQryOptions(string refDataKey);
    }
}