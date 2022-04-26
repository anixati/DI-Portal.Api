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
    public class EntityPatchHandler<T> : EntityHandler<T>, IRequestHandler<PatchEntityRequest<T>, DomainResponse>
        where T : class, IEntity
    {
        public EntityPatchHandler(IStore store, IMapper mapper) : base(store, mapper)
        {
        }

        public async Task<DomainResponse> Handle(PatchEntityRequest<T> request, CancellationToken cancellationToken)
        {
            Ensure.That(() => request.Id > 0, "id is required");
            var entity = await Repository.GetById(request.Id);
            entity.ThrowIfNull("Record not found or deleted !");

            request.Apply(entity);

            var result = await Repository.UpdateAsync(entity);
            Commit();
            return new DomainResponse(ResponseCode.Updated, "Patched");
        }
    }
}