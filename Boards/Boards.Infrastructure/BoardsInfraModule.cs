using System;
using Autofac;
using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Infrastructure.Domain;
using DI.Domain;
using DI.Domain.Core;
using DI.Forms;
using Di.Qry;
using DI.Security;

namespace Boards.Infrastructure
{
    public class BoardsInfraModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(BoardsInfraModule);
            var moduleAssembly = moduleType.Assembly;

            builder.AddVersion(ThisAssembly);

            //!!!! dont do this on this assembly!!!!
            //builder.RegisterAssemblyTypes(moduleAssembly)
            //    .AsImplementedInterfaces();


            builder.AddDbContext<BoardsDbContext>();
            builder.AddQryProviders<BoardsDbContext>();
            builder.AddFormProviders();
            builder.RegisterEntityHandlers<BoardsDbContext>(typeof(BaseEntity).Assembly, typeof(Board).Assembly);

            builder.RegisterType<BoardsContext>()
                .As<IBoardsContext>()
                .InstancePerLifetimeScope();

            builder.RegisterType<SecurityContext>()
                .As<ISecurityContext>()
                .InstancePerLifetimeScope();

            // builder.AddMappings(moduleType);
            // builder.AddEntityServices(moduleAssembly);
        }
    }
}