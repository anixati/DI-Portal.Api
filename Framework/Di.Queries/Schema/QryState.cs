﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Providers;
using Di.Qry.Schema.Types;
using DI.Queries;
using DI.Security;
using SqlKata;

namespace Di.Qry.Schema
{
    public class QryState : IQryState
    {
        private readonly Dictionary<string, QryField> _fds;
        private readonly List<SuQryState> _subQueries;
        private List<GridColumn> _cols;

        public QryState(Table table)
        {
            Table = table;
            _cols = new List<GridColumn>();
            _fds = new Dictionary<string, QryField>();
            Query = new Query(Table.TableName);
            _subQueries = new List<SuQryState>();
        }

        public Table Table { get; }
        public string ParentId { get; set; }
        public string TeamId { get; set; }
        public string Key => $"{Table.Name}_Query";

        public bool HasSubQueries => _subQueries.Any();

        public string Title { get; set; }

        public IQryContext Compile()
        {
            return QryContext.Create("Default", new LocalCompiler()
                .Compile(Query));
        }

        public IQryState OrderBy(string column, bool desc = false)
        {
            _ = desc
                ? Query.OrderByDesc(column)
                : Query.OrderBy(column);
            return this;
        }

        public IQryState Page(int limit, int offset)
        {
            Query.Limit(limit).Offset(offset);
            return this;
        }

        public IQryState Where(string column, string op, object value)
        {
            Query.Where(column, op, value);
            return this;
        }

        public IQryState Where(string column, object value)
        {
            Query.WhereRaw(column, value);
            return this;
        }

        public void AddSubQry(string key, string fromKey, string toKey, Func<Table> entityFunc)
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
            query.Join(link.Table.TableName, x =>
            {
                x.On(link.From, link.To);
                if (link.Clauses == null) return x;
                foreach (var craw in link.Clauses)
                    x.WhereRaw(craw);
                return x;
            }, link.JoinType);
        }


        private Query BuildSubQuery(QryField qfd, QryFilter rule, string queryOnField)
        {
            var subQuery = new Query(qfd.Table.TableName)
                .Select(queryOnField);
            foreach (var (_, link) in qfd.Table.Links)
                AddJoin(subQuery, link);
            var sqlFilter = qfd.Transalate(rule);
            subQuery.Where(qfd.QueryKey, sqlFilter.Operator, sqlFilter.Value);
            return subQuery;
        }


        #region Compile Paged Query

        public async Task<IPagedContext> Compile(IQryRequest request, ISecurityContext securityContext)
        {
            var rv = new PagedContext {PageInfo = request.PageInfo};
            var compiler = new LocalCompiler();
            var cols = GetQryColumns();
            if (!_fds.Any())
                SetUpQryFields(Table);

            var exQuery = Query.Clone();

            if (request.Filter is {HasChildRules: true})
                exQuery.Where(x => AddClause(request.Filter, request.Filter.IsOr, x));

            //Filter by parent id
            if (!string.IsNullOrEmpty(ParentId) && request.EntityId.HasValue)
                exQuery.Where(x => x.Where(ParentId, "=", request.EntityId.GetValueOrDefault()));

            //Filter by teamId
            if (!string.IsNullOrEmpty(TeamId) && !securityContext.IsSysAdmin())
            {
                var teams = await securityContext.GetTeamIds();
                if (teams.Any()) exQuery.Where(x => x.WhereIn(TeamId, teams));
                else exQuery.Where(x => x.WhereIn(TeamId, new long[] {0}));
            }

            if (request.CanSearch())
                exQuery.Where(q =>
                    {
                        foreach (var col in cols.Where(x => x.Searchable))
                            q.OrWhereLike(col.SortCol, $"%{request.SearchStr}%");
                        return q;
                    }
                );


            //  exQuery = cols.Where(x => x.Searchable).Aggregate(exQuery,
            // (current, col) => current.OrWhereLike(col.SortCol, $"%{request.SearchStr}%"));


            var countQuery = exQuery.Clone();
            var qry = countQuery.AsCount(new[] {$"{Table.PrimaryKey}"});
            rv.CountQry = QryContext.Create("Count", compiler.Compile(qry));

            foreach (var si in GetSortColumns(request, cols))
                exQuery = si.Desc ? exQuery.OrderByDesc(si.Id) : exQuery.OrderBy(si.Id);

            var pageSize = request.PageInfo.PageSize;
            var page = request.PageInfo.CurrentPage < 1 ? 0 : request.PageInfo.CurrentPage;
            var totalSkip = page * pageSize;
            exQuery.Skip(totalSkip < 0 ? 0 : totalSkip).Take(pageSize);

            rv.DataQry = QryContext.Create("Default", compiler.Compile(exQuery));
            return rv;
        }

        private List<SortInfo> GetSortColumns(IQryRequest request, List<GridColumn> cols)
        {
            var sortList = Table.SortColumns;
            if (request.SortInfos.Any())
                sortList = request.SortInfos;

            var rv = new List<SortInfo>();
            foreach (var si in sortList)
            {
                var col = cols.FirstOrDefault(x =>
                    string.Compare(x.Accessor, si.Id, StringComparison.OrdinalIgnoreCase) == 0);
                if (col != null)
                    rv.Add(new SortInfo(col.SortCol, si.Desc));
            }

            return rv;
        }

        private Query AddClause(QryFilter filter, bool isOr, Query query)
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

        private void AddRule(Query query, QryFilter rule, bool isOr)
        {
            if (!_fds.TryGetValue(rule.Field, out var qfd))
                throw new Exception($"Query field :{rule.Field} not permitted ");

            var clause = qfd.Transalate(rule);

            //check is it a subquery
            if (qfd.IsSubQry)
            {
                var queryOnField = qfd.EntityField; // Table.PrimaryKey;
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

        private Query Query { get; }

        public static QryState Create(Table qryTable)
        {
            var qs = new QryState(qryTable);
            SetupQuery(qryTable, qs.Query);
            return qs;
        }

        private static void SetupQuery(Table table, Query sqlQry)
        {
            foreach (var qc in table.Columns)
                if (qc.SelectType == SelectType.Raw)
                    sqlQry.SelectRaw(qc.ColName);
                else sqlQry.Select(qc.ColName);
            if (!table.Links.Any()) return;
            foreach (var (_, link) in table.Links)
            {
                AddJoin(sqlQry, link);
                SetupQuery(link.Table, sqlQry);
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

            if (!Query.Clauses.Any())
                Query.WhereRaw("1=?", 1);
        }

        #endregion


        #region Query Configuration

        public List<GridColumn> GetQryColumns()
        {
            _cols = new List<GridColumn>();
            SetUpColumns(Table);
            return _cols;
        }

        private void SetUpColumns(Table table)
        {
            foreach (var qf in table.Columns)
                _cols.Add(qf);
            if (!table.Links.Any()) return;
            foreach (var ql in table.Links.Values)
                SetUpColumns(ql.Table);
        }

        public Dictionary<string, IQryField> GetQryFields()
        {
            SetUpQryFields(Table);
            return _fds.ToDictionary(x => x.Key, y => (IQryField) y.Value);
        }

        private void SetUpQryFields(Table table)
        {
            foreach (var qf in table.QryFields)
                _fds[qf.QueryKey] = qf;
            if (!table.Links.Any()) return;
            foreach (var ql in table.Links.Values)
                SetUpQryFields(ql.Table);
        }

        #endregion
    }
}