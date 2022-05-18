using Autofac;
using Di.Qry.Core;
using Di.Qry.Providers;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Di.Qry
{
    public static class QryExtensions
        {

            #region Service providers


            public static void AddQryProviders<T>(this ContainerBuilder builder)
        {

            builder.Register(cx =>
                {
                    var configuration = cx.Resolve<IConfiguration>();
                    var connStr = configuration.GetConnectionString(typeof(T).Name);
                    return new SqlServerConnFactory(connStr);
                }).As<IQryConnFactory>()
                .SingleInstance();
            builder.RegisterType<SqlServerDataSource>()
                .As<IQryDataSource>()
                .InstancePerLifetimeScope();
            builder.RegisterType<QryProvider>()
                .As<IQryProvider>()
                .SingleInstance();

        }
        public static void AddQueries(this ContainerBuilder builder, Assembly assembly)
        {
            
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.IsAssignableTo<IQrySchema>() & !t.IsAbstract)
                .Keyed<IQrySchema>(t => t.Name)
                .AsImplementedInterfaces();
        } 
        #endregion
    }




}