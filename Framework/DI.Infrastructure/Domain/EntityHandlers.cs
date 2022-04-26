using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Autofac;
using Autofac.Core;
using DI.Domain.Core;
using DI.Domain.Handlers.Generic;
using DI.Domain.Requests;
using DI.Domain.Services;
using DI.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain
{
    public static class EntityHandlers

    {
        private static readonly List<(Type handler, Type request, Type response)> TypeList =
            new()
            {
                (typeof(EntityRequestHandler<>), typeof(Entity.Request<>), typeof(EntityResponse<>)),
                (typeof(EntityQueryHandler<>), typeof(Entity.Query<>), typeof(QueryResponse<>)),
                (typeof(EntityPatchHandler<>), typeof(PatchEntityRequest<>), typeof(DomainResponse)),
                (typeof(EntityStateHandler<>), typeof(Entity.ChangeState<>), typeof(ActionResponse))
            };


        public static void RegisterEntityHandlers<T>(this ContainerBuilder builder, Assembly assembly)
            where T : DbContext
        {
            builder.RegisterEntityHandlers<T>(assembly, TypeList);
        }


        public static void RegisterEntityHandlers<T>(this ContainerBuilder builder, Assembly assembly,
            List<(Type handler, Type request, Type response)> genericHandlers) where T : DbContext
        {
            var entityTypes = assembly?.GetTypes()?.Where(x => x.IsSubclassOf(typeof(BaseEntity)));
            if (entityTypes == null) return;

            foreach (var type in entityTypes)
            foreach (var tp in genericHandlers)
                builder.AddRequestHandler<T>(type, tp.handler, tp.request, tp.response);
        }

        private static void AddRequestHandler<T>(this ContainerBuilder builder, Type entity, Type handler, Type request,
            Type response) where T : DbContext
        {
            var rp = new ResolvedParameter(
                (p, c) => p.ParameterType == typeof(IStore),
                (p, c) => c.Resolve<IStore<T>>()
            );
            var reqType = request.MakeGenericType(entity);

            var resType = response.IsGenericType ? response.MakeGenericType(entity) : response;
            builder.RegisterType(handler.MakeGenericType(entity))
                .WithParameter(rp)
                .As(typeof(IRequestHandler<,>).MakeGenericType(reqType, resType));
        }
    }
}