using Autofac;
using Autofac.Core;
using DI.Domain.Core;
using DI.Domain.Handlers;
using DI.Domain.Services;
using DI.Response;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace DI.Domain
{
    public static class RegisterGenericHandlers

    {
        private static readonly List<(Type handler, Type request, Type response)> Handlers =
            new List<(Type handler, Type request, Type response)>
            {
                (typeof(EntityRequestHandler<>), typeof(Entity.Request<>), typeof(EntityResponse<>)),
                (typeof(EntityQueryHandler<>), typeof(Entity.Query<>), typeof(QueryResponse<>)),
                (typeof(EntityStateHandler<>), typeof(Entity.ChangeState<>), typeof(ActionResponse))
            };


        public static void AddEntityHandlers<T>(this ContainerBuilder builder, Assembly assembly) where T : DbContext
        {
            var entityTypes = assembly?.GetTypes()?.Where(x => x.IsSubclassOf(typeof(BaseEntity)));
            if (entityTypes == null) return;

            foreach (var type in entityTypes)
            foreach (var tp in Handlers)
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