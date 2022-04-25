using System;
using System.Linq;
using System.Linq.Expressions;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Data;
using Microsoft.EntityFrameworkCore.Query;

namespace DI.Domain.Queries
{
    public class QryBuilder<T>  where T : class, IEntity
    {
        private readonly QrySpec<T> _spec;
        private QryBuilder( bool tracking = false)
        {
            _spec = new QrySpec<T>(tracking);
        }

        public QryBuilder<T> Where(Expression<Func<T, bool>> expression)
        {
            _spec.AddCondition(expression);
            return this;
        }
        public QryBuilder<T> SetPaging(ISearchRequest request)
        {
            _spec.SetPaging(request.GetPageCookie());
            return this;
        }
        public QryBuilder<T> SetPaging(int? skip, int? take)
        {
            _spec.SetPaging(skip,take);
            return this;
        }
        public QryBuilder<T> SetPaging(PageCookie cookie)
        {
            _spec.SetPaging(cookie);
            return this;
        }
        public QryBuilder<T> AddIncludes(params string[] includes)
        {
            _spec.AddIncludes(includes);
            return this;
        }
        public QryBuilder<T> SetOrderBy(Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            _spec.SetOrderBy(orderBy);
            return this;
        }
        public QryBuilder<T> SetInclude(Func<IQueryable<T>, IIncludableQueryable<T, object>> func)
        {
            _spec.SetInclude(func);
            return this;
        }
        public IQrySpec<T> Build()
        {
            return _spec;
        }
        

        public static IQrySpec<T> Create(Action<QryBuilder<T>> build, bool tracking = false)
        {
            var qb = new QryBuilder<T>(tracking);
            build(qb);
            return qb.Build();
        }
    }
}
