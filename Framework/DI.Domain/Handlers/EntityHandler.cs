using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Services;

namespace DI.Domain.Handlers
{
    public abstract class EntityHandler<T> where T : class, IEntity
    {
        private readonly IMapper _mapper;

        protected EntityHandler(IDataStore dataStore, IMapper mapper)
        {
            Store = dataStore;
            _mapper = mapper;
        }

        protected IDataStore Store { get; }

        protected IRepository<T> Repository => Store.Repo<T>();

        protected void Commit()
        {
            Store.Commit();
        }
    }
}