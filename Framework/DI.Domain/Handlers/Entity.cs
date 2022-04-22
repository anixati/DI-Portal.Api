using System;
using DI.Actions;
using DI.Domain.Core;
using DI.Domain.Queries;
using DI.Response;
using MediatR;

namespace DI.Domain.Handlers
{
    public class Entity
    {
        public enum Action
        {
            Default = 0,
            Create = 1000,
            Retrieve = 1001,
            Update = 1002,
            Delete = 1003,
            Custom = 1004
        }

        public abstract class BaseRequest<T> where T : IEntity
        {
            public string EntityName => typeof(T).Name.ToKey();
        }


        public class Query<T> : BaseRequest<T>, IRequest<QueryResponse<T>> where T : class, IEntity
        {
            public Query(IQrySpec<T> expression)
            {
                Expression = expression;
            }

            public IQrySpec<T> Expression{ get;  }
        }


        public class ChangeState<T> : BaseRequest<T>, IRequest<ActionResponse> where T : IEntity
        {
            public ChangeState(SetStatusAction.ActionType action, long id, string reason)
            {
                Action = action switch
                {
                    SetStatusAction.ActionType.Delete => StatusAction.Delete,
                    SetStatusAction.ActionType.Disable => StatusAction.Disable,
                    SetStatusAction.ActionType.Enable => StatusAction.Enable,
                    SetStatusAction.ActionType.Lock => StatusAction.Lock,
                    SetStatusAction.ActionType.UnLock => StatusAction.UnLock,
                    _ => throw new Exception("Not valid")
                };
                ;
                Id = id;
                Reason = reason;
            }

            public StatusAction Action { get; }
            public long Id { get; }
            public string Reason { get; }
        }

        public class Request<T> : BaseRequest<T>, IRequest<EntityResponse<T>> where T : class, IEntity
        {
            public Request(T entity, Action action = Action.Create, bool commit = true)
            {
                Entity = entity;
                Commit = commit;
                Action = action;
            }

            public Request(long id)
            {
                Entity = null;
                Commit = false;
                Id = id;
                Action = Action.Retrieve;
            }

            public Action Action { get; }
            public bool Commit { get; }
            public long? Id { get; }
            public T Entity { get; }
        }
    }
}