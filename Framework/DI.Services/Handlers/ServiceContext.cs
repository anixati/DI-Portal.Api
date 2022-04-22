using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Handlers;
using DI.Domain.Queries;
using DI.Domain.Services;
using DI.Security;
using DI.Services.Core;
using MediatR;

namespace DI.Services.Handlers
{
    public class ServiceContext : IServiceContext
    {
        private readonly IIdentityProvider _identityProvider;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public ServiceContext(IMediator mediator, IIdentityProvider identityProvider, IMapper mapper)
        {
            _mediator = mediator;
            _identityProvider = identityProvider;
            _mapper = mapper;
        }

        public async Task<IPagedList<TK>> GetListByQry<T, TK>(Action<QryBuilder<T>> builder, bool tracking = false) where T : class, IEntity where TK : class, IViewModel
        {
            return await GetList<T, TK>(QryBuilder<T>.Create(builder, tracking));
        }

        public async Task<IPagedList<TK>> GetList<T,TK>(IQrySpec<T> spec) where T : class, IEntity where TK : class, IViewModel
        {
            var response = await _mediator.Send(new Entity.Query<T>(spec));
            response.ThrowIfNull($"Entity list response for {typeof(T).Name}");
            response.List.ThrowIfNull("Entity list response");
            response.List.Items.ThrowIfNull("Entity list response");
            var pgList = response.List;
            if (pgList == null) return new PagedList<TK>();
            var rl = _mapper.Map<List<T>, List<TK>>(pgList.Items);
            return PagedList<TK>.Create(rl, pgList.Total, pgList.PageIndex, pgList.PageSize);
        }


        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken = default)
        {
            return await _mediator.Send(request, cancellationToken);
        }
    }
}