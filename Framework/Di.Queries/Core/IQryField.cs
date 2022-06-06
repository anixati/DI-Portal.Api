using System.Collections.Generic;
using DI.Queries;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public interface IQryField
    {
        string Alias { get; }
        string FieldName { get; }
        string QueryKey { get; }
        string Name { get; }
        bool Nullable { get; }
        string ReferenceSchema { get; }
        List<QryOption> Options { get; }
        string[] Operators { get; }
        bool IsMetaData { get; }
        bool IsSubQry { get; }
        JObject GetConfig(IQryProvider provider);
        QryClause Transalate(IQryFilter filter);
    }
}