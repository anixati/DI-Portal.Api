using System;
using Autofac;
using Boards.Api;
using Boards.Domain;
using Boards.Infrastructure;
using Boards.Services;
using DI;
using DI.Services;
using DI.WebApi;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using System.Collections.Generic;
using System.Reflection;

namespace Boards.ApiHost
{
    internal static class Modules
    {
        internal static void AddModules(this ContainerBuilder builder)
        {
            var assembly = typeof(Modules).Assembly;
            builder.AddVersion(assembly);

            //-- Framework Modules ---
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ServiceModule());
            builder.RegisterModule(new WebApiModule());

            //-- Mocks Modules---
            //  builder.RegisterModule(new MockContractModule());

            //-- Domain Modules---
            builder.RegisterModule(new BoardsDomainModule());

            //-- Infrastructure Modules---
            builder.RegisterModule(new BoardsInfraModule());

            //-- Application Modules---
            builder.RegisterModule(new BoardsServiceModule());

            //-- Application Modules---
            builder.RegisterModule(new BoardsApiModule());
        }

        public static IEnumerable<AssemblyPart> GetControllers()
        {
            yield return new AssemblyPart(typeof(WebApiModule).GetTypeInfo().Assembly);
            yield return new AssemblyPart(typeof(BoardsApiModule).GetTypeInfo().Assembly);
        }

        //public static IEnumerable<IApplicationFeatureProvider> GetFeatures()
        //{
        //    yield return new ModelControllersFeature<SasiServiceModule>();
        //}


    }
}