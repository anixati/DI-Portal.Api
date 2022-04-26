using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using AutoMapper;
using MediatR;

namespace DI.Services.Core
{
    public static class Registrations
    {
        private static readonly Type[] OpenTypes =
        {
            typeof(IValueResolver<,,>), typeof(IMemberValueResolver<,,,>), typeof(ITypeConverter<,>),
            typeof(IMappingAction<,>)
        };

        public static void AddMediator(this ContainerBuilder builder)
        {
            builder.RegisterType<Mediator>().As<IMediator>()
                .InstancePerLifetimeScope();
            builder.Register<ServiceFactory>(context =>
            {
                var c = context.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        /// <summary>
        ///     Scan and register automapper configuration
        /// </summary>
        /// <param name="builder"></param>
        /// <param name="assemblyTypes"></param>
        public static void AddMappings(this ContainerBuilder builder, params Type[] assemblyTypes)
        {
            if (assemblyTypes == null) return;
            var assemblies = assemblyTypes.Select(x => x.Assembly).ToArray();
            builder.RegisterAssemblyTypes(assemblies)
                .AssignableTo(typeof(Profile))
                .As<Profile>()
                .SingleInstance();

            foreach (var openType in OpenTypes)
                builder.RegisterAssemblyTypes(assemblies)
                    .AsClosedTypesOf(openType)
                    .AsImplementedInterfaces()
                    .InstancePerDependency();
        }

        /// <summary>
        ///     register automapper
        /// </summary>
        /// <param name="builder"></param>
        public static void AddMappingCore(this ContainerBuilder builder)
        {
            builder.Register(cx =>
                    new MapperConfiguration(config =>
                    {
                        var profiles = cx.Resolve<IEnumerable<Profile>>();
                        foreach (var profile in profiles.Select(profile => profile.GetType()))
                            config.AddProfile(profile);
                    }))
                .As<IConfigurationProvider>()
                .AsSelf()
                .SingleInstance();

            builder.Register(cx =>
                    cx.Resolve<MapperConfiguration>()
                        .CreateMapper(cx.Resolve<IComponentContext>().Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }
    }
}