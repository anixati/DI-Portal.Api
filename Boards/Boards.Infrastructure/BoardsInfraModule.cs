﻿using System;
using Autofac;
using Boards.Infrastructure.Design;
using Boards.Infrastructure.Domain;
using DI.Domain;
using DI.Domain.Core;
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

            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();


            builder.RegisterType<MockUserIdentityProvider>().As<IIdentityProvider>();
            builder.AddDbContext<BoardsDbContext>();
            builder.RegisterEntityHandlers<BoardsDbContext>(typeof(BaseEntity).Assembly);
            // builder.AddMappings(moduleType);
            // builder.AddEntityServices(moduleAssembly);
        }
    }
}