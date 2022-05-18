using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Di.Qry.Core;
using Di.Qry.Providers;
using Di.Qry.Schema.Types;
using SqlKata;

namespace Di.Qry.Schema
{
    public class QryState : IQryState
    {
        private readonly Query _query;
        private readonly Dictionary<string, Field> _fds;
        private readonly List<SuQryState> _subQueries;
        private List<GridColumn> _cols;
        public QryState(Entity entity)
        {
            Entity = entity;
            _cols = new List<GridColumn>();
            _fds = new Dictionary<string, Field>();
            _query = new Query(Entity.TableName);
            _subQueries = new List<SuQryState>();
        }


        public Entity Entity { get; }

        public string Key => $"{Entity.Name}_Query";

        public bool HasSubQueries => _subQueries.Any();

        public IQryContext Compile()
        {
            return QryContext.Create("Default", new LocalCompiler()
                .Compile(_query));
        }

        public IQryState OrderBy(string column, bool desc = false)
        {
            _ = desc
                ? _query.OrderByDesc(column)
                : _query.OrderBy(column);
            return this;
        }

        public IQryState Page(int limit, int offset)
        {
            _query.Limit(limit).Offset(offset);
            return this;
        }

        public IQryState Where(string column, object value)
        {
            _query.WhereRaw(column, value);
            return this;
        }

        public void AddSubQry(string key, string fromKey, string toKey, Func<Entity> entityFunc)
        {
            _subQueries.Add(new SuQryState(key, fromKey, toKey, entityFunc));
        }

        public IEnumerable<SuQryState> SubQueries()
        {
            foreach (var subQryState in _subQueries)
                yield return subQryState;
        }

        private static void AddJoin(Query query, Link link)
        {
            //if (link.LinkType == LinkType.Outer)
            //    query.LeftJoin(link.Entity.TableName, link.From, link.To);
            //else
            //    query.Join(link.Entity.TableName, link.From, link.To);

            query.Join(link.Entity.TableName, x =>
            {
                x.On(link.From, link.To);
                if (link.Clauses != null)
                {
                    foreach (var craw in link.Clauses)
                        x.WhereRaw(craw);
                }
                return x;
            }, link.JoinType);
        }


        private Query BuildSubQuery(Field qfd, IQryFilter rule, string queryOnField)
        {
            var subQuery = new Query(qfd.Entity.TableName)
                .Select(queryOnField);
            foreach (var (_, link) in qfd.Entity.Links)
                AddJoin(subQuery, link);
            var sqlFilter = qfd.Transalate(rule);
            subQuery.Where(qfd.QueryKey, sqlFilter.Operator, sqlFilter.Value);
            return subQuery;
        }


        #region Compile Paged Query

        public IPagedContext Compile(IQryRequest request)
        {
            var rv = new PagedContext {PageInfo = request.PageInfo};
            var compiler = new LocalCompiler();

            if (!_fds.Any())
                SetUpQryFields(Entity);

            var exQuery = _query.Clone();

            if (request.Filter != null && request.Filter.HasChildRules)
                exQuery.Where(x => AddClause(request.Filter, request.Filter.IsOr, x));

            var countQuery = exQuery.Clone();
            var qry = countQuery.AsCount(new[] {$"{Entity.PrimaryKey}"});
            rv.CountQry = QryContext.Create("Count", compiler.Compile(qry));

            var sortList = Entity.SortColumns;
            if (request.SortInfos.Any())
                sortList = request.SortInfos;

            var cplQuery = exQuery;
            foreach (var si in sortList)
                cplQuery = si.Desc ? exQuery.OrderByDesc(si.Name) : exQuery.OrderBy(si.Name);

            var pageSize = request.PageInfo.PageSize;
            var page = request.PageInfo.CurrentPage < 1 ? 0 : request.PageInfo.CurrentPage;
            var totalSkip = (page) * pageSize;
            cplQuery.Skip(totalSkip < 0 ? 0 : totalSkip).Take(pageSize);

            rv.DataQry = QryContext.Create("Default", compiler.Compile(cplQuery));
            return rv;
        }

        private Query AddClause(IQryFilter filter, bool isOr, Query query)
        {
            if (!filter.HasChildRules)
                return query;

            foreach (var rule in filter.Rules)
                if (rule.IsRuleSet)
                    query.Where(q =>
                    {
                        AddClause(rule, rule.IsOr, q);
                        return q;
                    });
                else
                    AddRule(query, rule, isOr);
            return query;
        }

        private void AddRule(Query query, IQryFilter rule, bool isOr)
        {
            if (!_fds.TryGetValue(rule.Field, out var qfd))
                throw new Exception($"Query field :{rule.Field} not permitted ");

            var clause = qfd.Transalate(rule);

            //check is it a subquery
            if (qfd.IsSubQry)
            {
                var queryOnField = qfd.EntityField;// Entity.PrimaryKey;
                var subQuery = BuildSubQuery(qfd, rule, queryOnField);

                if (isOr)
                    query.OrWhereIn(queryOnField, subQuery);
                else
                    query.WhereIn(queryOnField, subQuery);
                return;
            }
            // is in rule
            if (rule.IsInRule)
            {
                var values = ParseValueStrArray(rule.Value);
                if (isOr)
                    query.OrWhereIn(qfd.QueryKey, values);
                else
                    query.WhereIn(qfd.QueryKey, values);
                return;
            }
            if (isOr)
                query.OrWhere(qfd.QueryKey, clause.Operator, clause.Value);
            else
                query.Where(qfd.QueryKey, clause.Operator, clause.Value);
        }

        private static List<string> ParseValueStrArray(object inpValue)
        {
            var enmValues = inpValue as IEnumerable;
            if (enmValues == null)
                return null;
            var rl = new List<string>();
            foreach (var val in enmValues) rl.Add(val.ToString());
            return rl;
        }


        #endregion


        #region Create Query State
        private Query Query => _query;

        public static QryState Create(Entity qryEntity)
        {
            var qs = new QryState(qryEntity);
            SetupQuery(qryEntity, qs.Query);
            return qs;
        }

        private static void SetupQuery(Entity entity, Query sqlQry)
        {
            foreach (var qc in entity.Columns)
                sqlQry.Select(qc.ColName);
            if (!entity.Links.Any()) return;
            foreach (var (_, link) in entity.Links)
            {
                AddJoin(sqlQry, link);
                SetupQuery(link.Entity, sqlQry);
            }
        }

        internal void FinaliseCreate()
        {
            foreach (var subQryState in _subQueries)
            {
                var qe = subQryState.EntFunc();
                var subQuery = new Query(qe.TableName);
                foreach (var qc in qe.Columns)
                    subQuery.Select(qc.ColName);
                foreach (var (_, link) in qe.Links)
                    AddJoin(subQuery, link);
                subQryState.SetQuery(subQuery);
            }
            if (!_query.Clauses.Any())
                _query.WhereRaw("1=?", 1);
        }

        #endregion


        #region Query Configuration

        public List<GridColumn> GetQryColumns()
        {
            SetUpColumns(Entity);
            return _cols;
        }

        private void SetUpColumns(Entity entity)
        {
            foreach (var qf in entity.Columns)
                _cols.Add(qf);
            if (!entity.Links.Any()) return;
            foreach (var ql in entity.Links.Values)
                SetUpColumns(ql.Entity);
        }

        public Dictionary<string, IQryField> GetQryFields()
        {
            SetUpQryFields(Entity);
            return _fds.ToDictionary(x => x.Key, y => (IQryField)y.Value);
        }

        private void SetUpQryFields(Entity entity)
        {
            foreach (var qf in entity.Fields)
                _fds[qf.QueryKey] = qf;
            if (!entity.Links.Any()) return;
            foreach (var ql in entity.Links.Values)
                SetUpQryFields(ql.Entity);
        }

        #endregion
    }

    public class SuQryState
    {
        private Query _query;

        public SuQryState(string key, string fromKey, string toKey, Func<Entity> entityFunc)
        {
            SchemaKey = key;
            FromKey = fromKey;
            ToKey = toKey;
            EntFunc = entityFunc;
        }

        public string SchemaKey { get; }
        public string FromKey { get; }
        public string ToKey { get; }
        public Func<Entity> EntFunc { get; }


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