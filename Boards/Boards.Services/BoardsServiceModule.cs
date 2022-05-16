using System;
using Autofac;
using Di.Qry;
using DI.Services.Core;

namespace Boards.Services
{
    public class BoardsServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(BoardsServiceModule);
            var moduleAssembly = moduleType.Assembly;

            builder.AddVersion(ThisAssembly);

            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();

             builder.AddMappings(moduleType);

             builder.AddQueries(moduleAssembly);


            // builder.AddEntityServices(moduleAssembly);
        }
    }
}