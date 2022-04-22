using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Services;

namespace DI.Domain.Handlers
{
    public abstract class EntityHandler<T> where T : class, IEntity
    {
        private readonly IMapper _mapper;
        private readonly IStore _store;

        protected EntityHandler(IStore store, IMapper mapper)
        {
            _store = store;
            _mapper = mapper;
        }

        protected IRepository<T> Repository => _store.Repo<T>();

        protected void Commit()
        {
            _store.Commit();
        }
    }
}