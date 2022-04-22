using System;
using Autofac;

namespace DI.WebApi
{
    public class WebApiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var moduleType = typeof(WebApiModule);
            var moduleAssembly = moduleType.Assembly;
            builder.AddVersion(moduleAssembly);
        }
    }
}