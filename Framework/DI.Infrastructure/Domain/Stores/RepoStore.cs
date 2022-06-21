using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;
using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Stores
{
    public class RepoStore<T> : DbQueryBase<T>, IRepository<T> where T : class, IEntity
    {
        public RepoStore(DbSet<T> set, IDataStore context) : base(set, context)
        {
        }

        public async Task<T> GetById(long id, bool includeAll = false)
        {
            if(includeAll)
                return await Active().IncludeAll().FirstOrDefaultAsync(x => x.Id == id);
            return await Active().FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> GetById(long id, params string[] includes)
        {
            var query = Active();
            if (includes != null)
                query = includes.Aggregate(query, (current, include) => current.Include(include));
            return await query.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<long> CountAsync()
        {
            return await Active(false).LongCountAsync();
        }

        public async Task<long> CountAsync(Expression<Func<T, bool>> expression)
        {
            return await Active(false).Where(expression).LongCountAsync();
        }

        public async Task<T> CreateAsync(T entity)
        {
            var entry = await Set.AddAsync(entity);
            return entry.Entity;
        }

        public async Task<T> CreateAndSaveAsync(T entity)
        {
            var entry = await Set.AddAsync(entity);
            await DataStore.SaveAsync();
            return entry.Entity;
        }

        public async Task CreateAsync(params T[] entities)
        {
            if (entities != null)
                await Set.AddRangeAsync(entities);
        }

        public async Task DeleteAsync(long id)
        {
            var entity = await GetById(id);
            entity.ThrowIfNull($"Failed to retrieve entity by id: {id}");
            Set.Remove(entity);
            await DataStore.SaveAsync();
        }

        public Task<bool> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public void DeleteAsync(params T[] entities)
        {
            if (entities != null)
                Set.RemoveRange(entities);
        }

        public async Task<T> FindAsync(long id, bool tracking = false)
        {
            return await Active(tracking).FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> condition, bool tracking = false)
        {
            return await Active(tracking).FirstOrDefaultAsync(condition);
        }

        public async Task<IEnumerable<T>> GetListAsync(bool tracking = false)
        {
            return await Active(tracking).ToListAsync();
        }

        public async Task<IEnumerable<TK>> GetListAsync<TK>(Expression<Func<T, TK>> selectExp,
            bool tracking = false)
            where TK : class
        {
            return await Active(tracking).Select(selectExp).ToListAsync();
        }

        public async Task<IEnumerable<T>> GetListAsync(Expression<Func<T, bool>> condition, bool tracking = false)
        {
            return await Active(tracking).Where(condition).ToListAsync();
        }

        public async Task<T> UpdateAsync(T entity)
        {
            
            var entry = Set.Update(entity);
            await DataStore.SaveAsync();
            return entry.Entity;
        }

        public void UpdateAsync(params T[] entities)
        {
            if (entities != null)
                Set.UpdateRange(entities);
        }

        public IQueryable<T> Query(bool tracking = false)
        {
            return Active(tracking);
        }

        public async Task<IPagedList<T>> GetListAsync(IQrySpec<T> qrySpec)
        {
            var listQry = qrySpec.Build(Active(qrySpec.Tracking));
            return await AsPagedList(qrySpec, listQry);
        }


        public async Task<IPagedList<TK>> GetListAsync<TK>(Expression<Func<T, TK>> selectExp, IQrySpec<T> qrySpec)
            where TK : class
        {
            var listQry = qrySpec.Build(Active(qrySpec.Tracking));
            return await AsPagedList(qrySpec, listQry, selectExp);
        }

        private static async Task<IPagedList<T>> AsPagedList(IQrySpec<T> qrySpec, IQueryable<T> listQry)
        {
            if (!qrySpec.IsPaged)
                return await AsPagedList(listQry);
            var total = await listQry.LongCountAsync();
            var pagedQry = ApplyPaging(qrySpec, listQry);
            var pagedItems = await pagedQry.ToListAsync();
            return PagedList<T>.Create(pagedItems, total, qrySpec.GetPageCookie());
        }

        private static IQueryable<T> ApplyPaging(IQrySpec<T> spec, IQueryable<T> inputQry)
        {
            var query = inputQry;
            if (spec.Skip.HasValue && spec.Take.HasValue)
            {
                var page = spec.Skip.GetValueOrDefault();
                var pageSize = spec.Take.GetValueOrDefault();

                var totalSkip = page * pageSize;
                query = query.Skip(totalSkip < 0 ? 0 : totalSkip).Take(pageSize);
            }

            return query;
        }


        private static async Task<IPagedList<TK>> AsPagedList<TK>(IQrySpec<T> qrySpec, IQueryable<T> listQry,
            Expression<Func<T, TK>> selectExp
        ) where TK : class
        {
            if (!qrySpec.IsPaged)
                return await AsPagedList(listQry, selectExp);
            var total = await listQry.LongCountAsync();
            var pagedQry = ApplyPaging(qrySpec, listQry);
            var pagedItems = await pagedQry.Select(selectExp).ToListAsync();
            return PagedList<TK>.Create(pagedItems, total, qrySpec.GetPageCookie());
        }

        public static async Task<PagedList<T>> AsPagedList(IQueryable<T> listQry)
        {
            var items = await listQry.ToListAsync();
            return AsPagedList(items);
        }

        public static async Task<PagedList<TK>> AsPagedList<TK>(IQueryable<T> listQry,
            Expression<Func<T, TK>> selectExp) where TK : class
        {
            var items = await listQry.Select(selectExp).ToListAsync();
            return AsPagedList(items);
        }

        public static PagedList<TK> AsPagedList<TK>(List<TK> data) where TK : class
        {
            var count = data.Count;
            return PagedList<TK>.Create(data, count, 1, 20);
        }

        public static PagedList<TK> AsPagedList<TK>(List<TK> data, long total, PageCookie cookie) where TK : class
        {
            return PagedList<TK>.Create(data, total, cookie.Index, cookie.Size);
        }
    }
}