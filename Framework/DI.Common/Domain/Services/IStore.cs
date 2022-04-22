using System;
using System.Threading.Tasks;
using DI.Domain.Core;

namespace DI.Domain.Services
{
    public interface IStore : IDisposable
    {
        IAppConfigStore AppConfigStore { get; }
        IAutoNumberStore AutoNumberStore { get; }
        Task BeginTransaction();
        void Commit();
        void Rollback();
        Task SaveAsync();
        IRepository<TK> Repo<TK>() where TK : class, IEntity;
    }
}