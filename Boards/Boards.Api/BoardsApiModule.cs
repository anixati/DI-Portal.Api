using System;
using Autofac;

namespace Boards.Api
{
    public class BoardsApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(BoardsApiModule);
            var moduleAssembly = moduleType.Assembly;

            builder.AddVersion(moduleAssembly);

            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();

            // builder.AddMappings(moduleType);
            // builder.AddEntityServices(moduleAssembly);
        }
    }
}