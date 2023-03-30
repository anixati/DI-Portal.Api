using System;
using System.Threading;
using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;
using DI.Domain.Requests;
using DI.Response;
using MediatR;

namespace DI.Services.Core
{
    public interface IServiceContext
    {
        Task<ActionResponse> ChangeStatus<T>(SetStatusAction request)
            where T : class, IEntity;

        Task<DomainResponse> PatchEntity<T>(PatchEntityRequest<T> request)
            where T : class, IEntity;

        Task<ViewResponse<TK>> Update<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel;

        Task<ViewResponse<TK>> Create<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel;

        Task<ViewResponse<TK>> GetById<T, TK>(long entityId)
            where T : class, IEntity where TK : class, IViewModel;

        Task<IPagedList<TK>> GetListByQry<T, TK>(Action<QryBuilder<T>> builder, bool tracking = false)
            where T : class, IEntity where TK : class, IViewModel;

        Task<IPagedList<TK>> GetList<T, TK>(IQrySpec<T> spec)
            where T : class, IEntity where TK : class, IViewModel;

        Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default);

        Task<ActionResponse> ChangeStatus<T>(Entity.ChangeState<T> request)
            where T : class, IEntity;
    }
}