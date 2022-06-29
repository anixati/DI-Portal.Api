using System;
using System.Collections.Generic;
using Di.Qry.Core;
using Di.Qry.Providers;
using Di.Qry.Schema.Types;
using SqlKata;

namespace Di.Qry.Schema
{
    public class SuQryState
    {
        private Query _query;

        public SuQryState(string key, string fromKey, string toKey, Func<Table> entityFunc)
        {
            SchemaKey = key;
            FromKey = fromKey;
            ToKey = toKey;
            EntFunc = entityFunc;
        }

        public string SchemaKey { get; }
        public string FromKey { get; }
        public string ToKey { get; }
        public Func<Table> EntFunc { get; }


        public void SetQuery(Query query)
        {
            _query = query;
        }

        public IQryContext Compile(IList<object> inSet)
        {
            if (inSet == null) return null;
            var query = _query.Clone();
            query.WhereIn(FromKey, inSet);

            var comResult = new LocalCompiler()
                .Compile(query);
            return QryContext.Create(SchemaKey, comResult);
        }
    }
}