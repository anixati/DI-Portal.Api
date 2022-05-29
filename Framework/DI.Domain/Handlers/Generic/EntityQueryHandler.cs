using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Requests;
using DI.Domain.Services;
using DI.Response;
using MediatR;

namespace DI.Domain.Handlers.Generic
{
    public class EntityQueryHandler<T> : EntityHandler<T>, IRequestHandler<Entity.Query<T>, QueryResponse<T>>
        where T : class, IEntity
    {
        public EntityQueryHandler(IDataStore dataStore, IMapper mapper) : base(dataStore, mapper)
        {
        }

        public async Task<QueryResponse<T>> Handle(Entity.Query<T> request, CancellationToken cancellationToken)
        {
            var result = await Repository.GetListAsync(request.Expression);
            return new QueryResponse<T>(ResponseCode.Default, "OK", result);
        }
    }
}