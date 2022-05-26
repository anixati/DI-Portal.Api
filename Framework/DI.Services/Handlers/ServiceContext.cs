using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DI.Actions;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;
using DI.Domain.Requests;
using DI.Response;
using DI.Security;
using DI.Services.Core;
using MediatR;

namespace DI.Services.Handlers
{
    public class ServiceContext : IServiceContext
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public ServiceContext(IMediator mediator, IIdentityProvider identityProvider, IMapper mapper)
        {
            _mediator = mediator;
            _identityProvider = identityProvider;
            _mapper = mapper;
        }


        public async Task<ViewResponse<TK>> Create<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel
        {
            var entity = _mapper.Map<TK, T>(model);

            change?.Invoke(entity);
            var changeRequest = new Entity.Request<T>(entity);
            var response = await _mediator.Send(changeRequest);

            return ConvertTo<T, TK>(response);
        }

        public async Task<ViewResponse<TK>> Update<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel
        {
            var entity = _mapper.Map<TK, T>(model);
            change?.Invoke(entity);
            var changeRequest = new Entity.Request<T>(entity, Entity.Action.Update);
            var response = await _mediator.Send(changeRequest);

            return ConvertTo<T, TK>(response);
        }

        public async Task<DomainResponse> PatchEntity<T>(PatchEntityRequest<T> request) where T : class, IEntity
        {
            return await _mediator.Send(request);
        }


        public async Task<ActionResponse> ChangeStatus<T>(SetStatusAction request) where T : class, IEntity
        {
            var stateRequest = new Entity.ChangeState<T>(request.Action, request.Id, request.Reason);
            return await _mediator.Send(stateRequest);
        }

        public async Task<ViewResponse<TK>> GetById<T, TK>(long entityId)
            where T : class, IEntity where TK : class, IViewModel
        {
            var request = new Entity.Request<T>(entityId);
            var response = await _mediator.Send(request);
            return ConvertTo<T, TK>(response);
        }

        public async Task<IPagedList<TK>> GetListByQry<T, TK>(Action<QryBuilder<T>> builder, bool tracking = false)
            where T : class, IEntity where TK : class, IViewModel
        {
            return await GetList<T, TK>(QryBuilder<T>.Create(builder, tracking));
        }

        public async Task<IPagedList<TK>> GetList<T, TK>(IQrySpec<T> spec)
            where T : class, IEntity where TK : class, IViewModel
        {
            var response = await _mediator.Send(new Entity.Query<T>(spec));
            response.ThrowIfNull($"Table list response for {typeof(T).Name}");
            response.List.ThrowIfNull("Table list response");
            response.List.Items.ThrowIfNull("Table list response");
            var pgList = response.List;
            if (pgList == null) return new PagedList<TK>();
            var rl = _mapper.Map<List<T>, List<TK>>(pgList.Items);
            return PagedList<TK>.Create(rl, pgList.Total, pgList.PageIndex, pgList.PageSize);
        }

        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request,
            CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }


        #region Helpers

        private ViewResponse<TK> ConvertTo<T, TK>(EntityResponse<T> response)
            where T : class, IEntity where TK : class, IViewModel
        {
            response.ThrowIfNull($"Table response for {typeof(T).Name}");
            if (response.Item == null) return new ViewResponse<TK>(response.ResponseCode, response.Message, null);
            var model = _mapper.Map<T, TK>(response.Item);
            return new ViewResponse<TK>(response.ResponseCode, response.Message, model);
        }

        #endregion
    }
}