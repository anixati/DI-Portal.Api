using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Services;

namespace DI.Domain.Handlers
{
    public abstract class EntityHandler<T> where T : class, IEntity
    {
        private readonly IMapper _mapper;
        private readonly IDataStore _dataStore;

        protected EntityHandler(IDataStore dataStore, IMapper mapper)
        {
            _dataStore = dataStore;
            _mapper = mapper;
        }

        protected IDataStore Store => _dataStore;
        protected IRepository<T> Repository => _dataStore.Repo<T>();

        protected void Commit()
        {
            _dataStore.Commit();
        }
    }
}