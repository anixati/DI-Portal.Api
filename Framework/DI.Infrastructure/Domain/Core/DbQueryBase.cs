using System;
using System.Linq;
using System.Linq.Expressions;
using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Core
{
    public abstract class DbQueryBase<T> where T : class, IEntity
    {
        protected DbQueryBase(DbSet<T> set, IStore store)
        {
            Set = set;
            Store = store;
        }

        protected DbSet<T> Set { get; }
        protected IStore Store { get; }
        protected IQueryable<T> TrQuery => Set;
        protected IQueryable<T> NtQuery => TrQuery.AsNoTracking();

        protected IQueryable<T> ActiveQuery => Active(false);

        protected IQueryable<T> Qry(bool tracking = true)
        {
            return tracking ? TrQuery : NtQuery;
        }

        protected IQueryable<T> Active(bool tracking = true)
        {
            return Qry(x => x.Deleted == false);
        }

        protected IQueryable<T> Qry(Expression<Func<T, bool>> expression, bool tracking = true)
        {
            return Qry(tracking).Where(expression);
        }
    }
}