using System.Threading.Tasks;
using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Services;

namespace Boards.Infrastructure.Domain
{
    public class BoardsContext : IBoardsContext
    {
        private readonly IDataStore<BoardsDbContext> _dataStore;
        public BoardsContext(IDataStore<BoardsDbContext> dataStore)
        {
            _dataStore = dataStore;
        }

        public IDataStore Store => _dataStore;
        public IRepository<T> Repo<T>() where T : class, IEntity
        {
            return _dataStore.Repo<T>();
        }
    }
}