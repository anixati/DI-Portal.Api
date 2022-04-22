using System;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Response;
using MediatR;

namespace DI.Domain.Handlers
{
    public class EntityRequestHandler<T> : EntityHandler<T>, IRequestHandler<Entity.Request<T>, EntityResponse<T>>
        where T : class, IEntity
    {
        public EntityRequestHandler(IMapper mapper, IStore store) : 
            base( store, mapper)
        {
        }

        public async Task<EntityResponse<T>> Handle(Entity.Request<T> request, CancellationToken cancellationToken)
        {
            return request.Action switch
            {
                Entity.Action.Retrieve => await RetrieveEntity(request),
                Entity.Action.Create => await CreateEntity(request),
                Entity.Action.Update => await UpdateEntity(request),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        private async Task<EntityResponse<T>> RetrieveEntity(Entity.Request<T> request)
        {
            Ensure.That(() => request.Id.HasValue, "id is null");
            var result = await Repository.GetById(request.Id.GetValueOrDefault());
            return new EntityResponse<T>(ResponseCode.Default, "retrieved", result);
        }

        private async Task<EntityResponse<T>> CreateEntity(Entity.Request<T> request)
        {
            //    await OnPreEvent(CoreEvent.Create, request.Entity);
            var result = await Repository.CreateAsync(request.Entity);
              //  await OnPostEvent(CoreEvent.Create, result);
            if (request.Commit)
                Commit();
            return new EntityResponse<T>(ResponseCode.Created, "Created", result);
        }

        private async Task<EntityResponse<T>> UpdateEntity(Entity.Request<T> request)
        {
              //  await OnPreEvent(CoreEvent.Update, request.Entity);
            var result = await Repository.UpdateAsync(request.Entity);
            //    await OnPostEvent(CoreEvent.Update, result);
            if (request.Commit)
                Commit();
            return new EntityResponse<T>(ResponseCode.Updated, "Updated", result);
        }
    }
}