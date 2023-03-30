using System;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Domain.Users;
using DI.Security.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Seed
{
    public abstract class DataSetupBase<T> : IDisposable where T : DbContext
    {
        protected readonly IDataStore<T> _dataStore;

        protected DataSetupBase(IDataStore<T> dataStore)
        {
            _dataStore = dataStore;
        }

        //protected DataSetupBase(IServiceProvider serviceProvider)
        //{
        //    DataStore = serviceProvider.GetRequiredService<IDataStore<T>>();
        //}

        public void Dispose()
        {
            _dataStore?.Dispose();
        }

        public async Task Run()
        {
            try
            {
                await _dataStore.BeginTransaction();
                await SetupSecurity();
                await SetupDomainData();
                await _dataStore.SaveAsync();
                _dataStore.Commit();
            }
            catch (Exception ex)
            {
                _dataStore.Rollback();
                Console.WriteLine($"ROLLING BACK ---{ex}");
                throw;
            }
        }

        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return _dataStore.Repo<TK>();
        }

        protected abstract Task SetupDomainData();

        protected async Task<TK> CreateIfNotExists<TK>(TK entity) where TK : class, INamedEntity
        {
            var repo = _dataStore.Repo<TK>();
            var op = await repo.FindAsync(x => EF.Functions.Like(x.Name, entity.Name)) ??
                     await repo.CreateAsync(entity);
            return op;
        }


        protected async Task<TK> Create<TK>(TK entity) where TK : class, IEntity
        {
            var repo = _dataStore.Repo<TK>();
            var op = await repo.CreateAsync(entity);
            return op;
        }

        protected async Task Save()
        {
            await _dataStore.SaveAsync();
        }

        public async Task SetupSecurity()
        {
            var opr = GetRepo<AppRole>();
            foreach (ApplicationRoles role in Enum.GetValues(typeof(ApplicationRoles)))
            {
                var entity = await CreateIfNotExists(new AppRole
                {
                    Name = $"{role}", Code = $"{(int) role}", Description = role.ToDesc(), Locked = true,
                    IsSystem = true
                });
            }

            await Save();
            var team = await CreateIfNotExists(new AppTeam
            {
                Name = "Default",
                Description = "Default Team",
                Locked = true,
                IsSystem = true
            });


            var entitites = _dataStore.Db.Model.GetEntityTypes().Select(x => x.ClrType).ToList();
            foreach (var entity in entitites)
                await CreateIfNotExists(new AppResource {Name = $"{entity.Name}", Description = $"{entity.FullName}"});


            await Save();
        }
    }
}