using System;
using Autofac;
using Di.Qry;
using DI.Services.Core;

namespace DI.Services
{
    public class ServiceModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(ServiceModule);
            var moduleAssembly = moduleType.Assembly;
            builder.AddVersion(moduleAssembly);

            //add mediater
            builder.AddMediator();

            // add auto mapper
            builder.AddMappingCore();
            builder.AddMappings(moduleType);

            

            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();
        }
    }
}