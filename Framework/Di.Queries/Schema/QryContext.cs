using System.Collections.Generic;
using System.Linq;
using Di.Qry.Contracts;
using SqlKata;

namespace Di.Qry.Schema
{
    public class QryContext : IQryContext
    {
        public string SqlString { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();

        public static QryContext Create(SqlResult result)
        {
            var rv = new QryContext
            {
                SqlString = result.Sql
            };
            if (result.NamedBindings != null)
                rv.Parameters = result.NamedBindings.ToDictionary(x =>
                    x.Key.Replace(":", ""), p => p.Value);
            return rv;

        }
    }
}