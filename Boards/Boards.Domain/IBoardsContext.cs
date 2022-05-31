using DI.Domain.Core;
using DI.Domain.Services;

namespace Boards.Domain
{
    public interface IBoardsContext
    {
        IDataStore Store { get; }
        IRepository<T> Repo<T>() where T : class, IEntity;
    }
}