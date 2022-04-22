using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Routing;

namespace DI.WebApi.Routes
{
    public static class RouteExtensions
    {

        public static void UseRoutePrefix(this MvcOptions opts, IRouteTemplateProvider routeAttribute)
        {
            opts.Conventions.Add(new PrefixConvention(routeAttribute));
        }

        public static void UseRoutePrefix(this MvcOptions opts, string prefix)
        {
            opts.UseRoutePrefix(new RouteAttribute(prefix));
        }

    }
}
