using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace DI.Domain.Queries
{
    public class QrySpec<T>: IQrySpec<T> where T : class, IEntity
    {
        private readonly List<Expression<Func<T, bool>>> _conditions = new List<Expression<Func<T, bool>>>();
        private Func<IQueryable<T>, IIncludableQueryable<T, object>> _incFunc;
        private List<string> _incStrs;
        private Func<IQueryable<T>, IOrderedQueryable<T>> _orderBy;
        public QrySpec(bool tracking)
        {
            Tracking = tracking;
            Skip = null;
            Take = null;
        }

        public bool Tracking { get; }
        public bool IsPaged => Skip.HasValue && Take.HasValue;
        public int? Skip { get; private set; }
        public int? Take { get; private set; }

        public PageCookie GetPageCookie()
        {
            return IsPaged ? new PageCookie(Skip.GetValueOrDefault(), Take.GetValueOrDefault()) : null;
        }


        public void SetPaging(int? skip,int? take)
        {
            this.Skip = skip;
            this.Take = take;
        }
        public void SetPaging(PageCookie cookie)
        {
            this.Skip = cookie.Index;
            this.Take = cookie.Size;
        }

        public IQueryable<T> Build(IQueryable<T> inputQry)
        {
            var query = inputQry;
            if (_conditions.Any())
                query = _conditions.Aggregate(query, (cr, cd) => cr.Where(cd));

            if (_incFunc != null)
                query = _incFunc(query);
            if (_incStrs != null && _incStrs.Any())
                query = _incStrs.Aggregate(query, (c, inc) => c.Include(inc));
            if (_orderBy != null)
                query = _orderBy(query);
            return query;
        }

        internal void AddCondition(Expression<Func<T, bool>> expression)
        {
            _conditions.Add(expression);
        }


        public void AddIncludes(params string[] includes)
        {
            _incStrs ??= new List<string>();
            if (includes != null)
                _incStrs.AddRange(includes.ToArray());
        }

        public void SetOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _orderBy = orderBy;
        }
        public void SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> func)
        {
            _incFunc = func;
        }

    }
}