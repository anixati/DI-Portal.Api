using System;
using Autofac;
using DI.Security;
using DI.WebApi.Security;

namespace DI.WebApi
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(WebApiModule);
            var moduleAssembly = moduleType.Assembly;
            builder.AddVersion(moduleAssembly);
            builder.RegisterAssemblyTypes(moduleAssembly)
                .AsImplementedInterfaces();

            builder.RegisterType<UserIdentityProvider>().As<IIdentityProvider>();
        }
    }
}