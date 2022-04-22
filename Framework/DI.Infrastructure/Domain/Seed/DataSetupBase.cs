using System;
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
        protected readonly IStore<T> Store;
        protected DataSetupBase(IServiceProvider serviceProvider)
        {
            Store = serviceProvider.GetRequiredService<IStore<T>>();
        }

        public void Dispose()
        {
            Store?.Dispose();
        }

        public async Task Run()
        {
            await Task.Delay(1);

            await SetupDomainData();

        }
        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return Store.Repo<TK>();
        }
        protected abstract Task SetupDomainData();

        protected async Task<TK> CreateIfNotExists<TK>(TK entity) where TK : class, INamedEntity
        {
            var repo= Store.Repo<TK>();
            var op = await repo.FindAsync(x => EF.Functions.Contains(x.Name, entity.Name)) ?? await repo.CreateAsync(entity);
            return op;
        }


        public async Task SetupSecurity()
        {
            var opr = GetRepo<AppRole>();
            foreach (ApplicationRoles role in Enum.GetValues(typeof(ApplicationRoles)))
            {
                var entity = await CreateIfNotExists(new AppRole() { Name = $"{role}", Code = $"{role.ToString().ToCode()}", Description = role.ToDesc()});


            }



            await Task.Delay(1);
        }


    }


}
