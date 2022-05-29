using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using DI.Core;
using DI.Domain.App;
using DI.Domain.Config;
using DI.Domain.Core;
using DI.Domain.Stores;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

namespace DI.Domain.Services
{
    public abstract class DbDataStoreBase<T> : ComponentBase, IDataStore<T> where T : DbContext
    {
        private readonly Dictionary<TypeInfo, dynamic> _repositories;
        private IAppConfigStore _appConfigStore;
        private IAutoNumberStore _autoNumberStore;
        private IDbContextTransaction _dbTransaction;

        protected DbDataStoreBase(T context, ILoggerFactory loggerFactory) : base(
            loggerFactory)
        {
            Db = context;
            _repositories = new Dictionary<TypeInfo, dynamic>();
        }

        public T Db { get; }

        public IAppConfigStore AppConfigStore =>
            _appConfigStore ??= new AppConfigStore(Db.Set<AppConfigEntity>(), this);

        public IAutoNumberStore AutoNumberStore =>
            _autoNumberStore ??= new AutoNumberStore(Db.Set<AutoNumberEntity>(), this);

        public async Task SaveAsync()
        {
            await Db.SaveChangesAsync();
        }

        public async Task BeginTransaction()
        {
            _dbTransaction = await Db.Database.BeginTransactionAsync();
        }

        public IRepository<TK> Repo<TK>() where TK : class, IEntity
        {
            var typeInfo = typeof(TK).GetTypeInfo();
            if (_repositories.ContainsKey(typeInfo))
                return _repositories[typeInfo];
            var repo = ResolveRepo<TK>();
            _repositories[typeInfo] = repo;
            return repo;
        }

        public void Commit()
        {
            try
            {
                _dbTransaction?.Commit();
            }
            finally
            {
                _dbTransaction?.Dispose();
            }
        }

        public void Rollback()
        {
            _dbTransaction?.Rollback();
            _dbTransaction?.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        private IRepository<TK> ResolveRepo<TK>() where TK : class, IEntity
        {
            return new RepoStore<TK>(Db.Set<TK>(), this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            _dbTransaction?.Dispose();
            Db?.Dispose();
        }
    }
}