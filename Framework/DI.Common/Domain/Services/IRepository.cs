using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;

namespace DI.Domain.Services
{
    public interface IRepository<T> where T : class, IEntity
    {
        IQueryable<T> Query(bool tracking = false);
        Task<T> GetById(long id,bool includeAll =false);
        Task<IEnumerable<T>> GetListAsync(bool tracking = false);

        Task<IEnumerable<TK>> GetListAsync<TK>(Expression<Func<T, TK>> selectExp, bool tracking = false)
            where TK : class;

        Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition, bool tracking = false);
        Task<IPagedList<T>> GetListAsync(IQrySpec<T> qrySpec);

        Task<IPagedList<TK>> GetListAsync<TK>(Expression<Func<T, TK>> selectExp, IQrySpec<T> qrySpec)
            where TK : class;

        Task<T> FindAsync(long id, bool tracking = false);
        Task<T> FindAsync(Expression<Func<T, bool>> condition, bool tracking = false);
        Task<long> CountAsync();
        Task<long> CountAsync(Expression<Func<T, bool>> expression);

        Task<T> CreateAsync(T entity);
        Task<T> CreateAndSaveAsync(T entity);
        Task CreateAsync(params T[] entities);

        Task DeleteAsync(long id);
        void DeleteAsync(params T[] entities);

        Task<T> UpdateAsync(T entity);
        void UpdateAsync(params T[] entities);
    }
}