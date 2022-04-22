using System;
using Autofac;
using DI;

namespace Boards.Domain
{
    public class BoardsDomainModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(BoardsDomainModule);
            var moduleAssembly = moduleType.Assembly;

            builder.AddVersion(ThisAssembly);

            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();

            // builder.AddMappings(moduleType);
            // builder.AddEntityServices(moduleAssembly);
        }
    }
}