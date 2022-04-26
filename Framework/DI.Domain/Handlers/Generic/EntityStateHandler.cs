using System.Threading;
using System.Threading.Tasks;
using DI.Core;
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
        private readonly IStore _store;

        public EntityStateHandler(IStore store, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _store = store;
        }

        public async Task<ActionResponse> Handle(Entity.ChangeState<T> request, CancellationToken cancellationToken)
        {
            var repo = _store.Repo<T>();
            var entity = await repo.GetById(request.Id);
            entity.ThrowIfNull("no record found!");

            EntityStateMachine.Set(entity, request.Action);

            var ue = await repo.UpdateAsync(entity);

            var code = GetCode(request.Action);
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