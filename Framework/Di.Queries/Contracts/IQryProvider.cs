using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Contracts
{
    public interface IQryProvider
    {
        IEnumerable<string> GetSchemaList();
        JObject GetSchemas();
        JObject GetConfig(string schemaKey);
        IQryState GetState(string qryStateKey);
    }
}