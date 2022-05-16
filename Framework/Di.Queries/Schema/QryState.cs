using System;
using System.Collections.Generic;
using System.Linq;
using Di.Qry.Contracts;
using Di.Qry.Core;
using SqlKata;

namespace Di.Qry.Schema
{
    public class QryState : IQryState
    {
        private readonly Dictionary<string, Field> _fds;
        private readonly Query _query;

        public QryState(Entity entity)
        {
            Entity = entity;
            _fds = new Dictionary<string, Field>();
            _query = new Query(entity.TableName);
        }

        public Entity Entity { get; }
        public string Key => $"{Entity.Name}_Query";

        #region Query Configuration
        public Dictionary<string, Field> GetFields()
        {
            SetUpQryFields(Entity);
            return _fds;
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

        public PagedContext Compile(IQryFilter filter, PageInfo pageInfo)
        {
            var rv = new PagedContext { PageInfo = pageInfo };
            var compiler = new LocalCompiler();
            if (!_fds.Any())
                SetUpQryFields(Entity);
            var exQuery = _query.Clone();
            if (filter == null || !filter.HasChildRules)
            {
                //  rv.DataQry = QryContext.Create(compiler.Compile(exQuery));
            }
            else
            {
                AddClause(filter, filter.IsOr, exQuery);
                //   rv.DataQry = QryContext.Create(compiler.Compile(exQuery));
            }


            var countQuery = exQuery.Clone();
            rv.CountQry = QryContext.Create(compiler.Compile(countQuery.AsCount()));
            var cpIdx = pageInfo.CurrentPage < 1 ? 1 : (pageInfo.CurrentPage - 1);
            var offset = pageInfo.PageSize * cpIdx;

            var cplQuery = exQuery.OrderBy(Entity.SortColumns.FirstOrDefault())
                .Limit(pageInfo.PageSize).Offset(offset);
            rv.DataQry = QryContext.Create(compiler.Compile(cplQuery));
            return rv;
        }
        public IQryContext Compile()
        {
            return QryContext.Create(new LocalCompiler()
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
        internal void AddSelect(Entity entity)
        {
            foreach (var qc in entity.Columns)
                _query.Select(qc.Column);
        }
        internal void Join(Link link)
        {
            if (link.LinkType == LinkType.Outer)
                _query.LeftJoin(link.Entity.TableName, link.From, link.To);
            else
                _query.Join(link.Entity.TableName, link.From, link.To);
        }
        private void AddClause(IQryFilter filter, bool isOr, Query query)
        {
            if (!filter.HasChildRules)
                return;
            foreach (var rule in filter.Rules)
                if (rule.IsRuleset)
                    query.Where(q =>
                    {
                        AddClause(rule, rule.IsOr, q);
                        return q;
                    });
                else
                    AddRule(query, rule, isOr);

            //query.Where(q =>
            //{
            //    foreach (var rule in filter.Rules)
            //    {
            //        if (rule.IsRuleset)
            //        {
            //           AddClause(rule, rule.IsOr, q);
            //        }
            //        else
            //        {
            //            AddRule(q,rule,isor);
            //        }
            //    }
            //    return q;
            //});
        }
        private void AddRule(Query query, IQryFilter rule, bool isOr)
        {
            if (!_fds.TryGetValue(rule.Field, out var qfd))
                throw new Exception($"Query field :{rule.Field} not permitted ");
            if (isOr)
                query.OrWhere(qfd.QueryKey, rule.Operator, rule.Value);
            else
                query.Where(qfd.QueryKey, rule.Operator, rule.Value);
        }
    }
}