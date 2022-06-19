using System;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Domain.Users;
using DI.Security.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace DI.Domain.Seed
{
    public abstract class DataSetupBase<T> : IDisposable where T : DbContext
    {
        protected readonly IDataStore<T> DataStore;

        protected DataSetupBase(IServiceProvider serviceProvider)
        {
            DataStore = serviceProvider.GetRequiredService<IDataStore<T>>();
        }

        public void Dispose()
        {
            DataStore?.Dispose();
        }

        public async Task Run()
        {
            try
            {
                await DataStore.BeginTransaction();
                await SetupSecurity();
                await SetupDomainData();
                await DataStore.SaveAsync();
                DataStore.Commit();
            }
            catch (Exception ex)
            {
                DataStore.Rollback();
                Console.WriteLine($"ROLLING BACK ---{ex}");
                throw;
            }
        }

        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return DataStore.Repo<TK>();
        }

        protected abstract Task SetupDomainData();

        protected async Task<TK> CreateIfNotExists<TK>(TK entity) where TK : class, INamedEntity
        {
            var repo = DataStore.Repo<TK>();
            var op = await repo.FindAsync(x => EF.Functions.Like(x.Name, entity.Name)) ??
                     await repo.CreateAsync(entity);
            return op;
        }


        protected async Task<TK> Create<TK>(TK entity) where TK : class, IEntity
        {
            var repo = DataStore.Repo<TK>();
            var op = await repo.CreateAsync(entity);
            return op;
        }

        public async Task SetupSecurity()
        {
            var opr = GetRepo<AppRole>();
            foreach (ApplicationRoles role in Enum.GetValues(typeof(ApplicationRoles)))
            {
                var entity = await CreateIfNotExists(new AppRole
                {
                    Name = $"{role}", Code = $"{role.ToString().ToCode()}", Description = role.ToDesc(), Locked = true
                });
            }

            var team = await CreateIfNotExists(new AppTeam
            {
                Name = $"Default",
                Description = "Default Team",
                Locked = true
            });


            var entitites = DataStore.Db.Model.GetEntityTypes().Select(x => x.ClrType).ToList();
            foreach (var entity in entitites)
                await CreateIfNotExists(new AppResource {Name = $"{entity.Name}", Description = $"{entity.FullName}"});


            await DataStore.SaveAsync();
        }
    }
}