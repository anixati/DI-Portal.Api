using System.Reflection;
using System.Xml;
using Autofac;
using Di.Qry.Contracts;
using Di.Qry.Providers;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Di.Qry
{
    public static class QryExtentions
    {
        public static void AddQryProviders<T>(this ContainerBuilder builder)
        {

            builder.Register(cx =>
                {
                    var configuration = cx.Resolve<IConfiguration>();
                    var connStr = configuration.GetConnectionString(typeof(T).Name);
                    return new SqlServerConnFactory(connStr);
                }).As<IQryConnFactory>()
                .SingleInstance();
            builder.RegisterType<SqlServerDbProvider>()
                .As<IQryDbProvider>()
                .InstancePerLifetimeScope();
            builder.RegisterType<QryProvider>()
                .As<IQryProvider>()
                .SingleInstance();

        }
        public static void AddQueries(this ContainerBuilder builder,Assembly assembly)
        {
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IQrySchema>())
                .Named<IQrySchema>(t => t.Name)
                .AsImplementedInterfaces();
        }
    }




}