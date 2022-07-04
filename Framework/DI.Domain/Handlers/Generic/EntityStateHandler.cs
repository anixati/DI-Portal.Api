using System;
using System.Threading;
using System.Threading.Tasks;
using DI.Core;
using DI.Domain.Activities;
using DI.Domain.Core;
using DI.Domain.Requests;
using DI.Domain.Services;
using DI.Response;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Domain.Handlers.Generic
{
    public class EntityStateHandler<T> : ComponentBase, IRequestHandler<Entity.ChangeState<T>, ActionResponse>
        where T : class, IEntity
    {
        private readonly IDataStore _dataStore;

        public EntityStateHandler(IDataStore dataStore, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _dataStore = dataStore;
        }

        public async Task<ActionResponse> Handle(Entity.ChangeState<T> request, CancellationToken cancellationToken)
        {
            var code = GetCode(request.Action);
            var repo = _dataStore.Repo<T>();
            var entity = await repo.GetById(request.Id);
            entity.ThrowIfNull("no record found!");
            if (entity is ICheckSystemEntity sysEntity)
            {
                if (sysEntity.IsSystem && request.Action == StatusAction.Delete)
                    throw new Exception($"System entities cannot be deleted ");
            }
            EntityStateMachine.Set(entity, request.Action);

            var ue = await repo.UpdateAsync(entity);
            if (code != ResponseCode.Deleted) return new ActionResponse(code, $"{code}", ue.Id);
            var dr = _dataStore.Repo<DeleteRecord>();
            var erf = entity.Reference();
            await dr.CreateAndSaveAsync(new DeleteRecord
            {
                EntityId = erf.Id,
                EntityName = erf.Name,
                Notes = request.Reason
                    
            });

            return new ActionResponse(code, $"{code}", ue.Id);
        }

        private ResponseCode GetCode(StatusAction action)
        {
            return action switch
            {
                StatusAction.Enable => ResponseCode.Enabled,
                StatusAction.Disable => ResponseCode.Disabled,
                StatusAction.Lock => ResponseCode.Locked,
                StatusAction.UnLock => ResponseCode.Unlocked,
                StatusAction.Delete => ResponseCode.Deleted,
                _ => ResponseCode.Default
            };
        }
    }
}