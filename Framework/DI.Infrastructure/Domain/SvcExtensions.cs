using System.Reflection;
using Autofac;
using DI.Domain.Services;
using DI.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DI.Domain
{
    public static class SvcExtensions
    {
        public static void AddDbContext<T>(this ContainerBuilder builder) where T : DbContext
        {
            var assembly = typeof(T).Assembly;

            builder.Register(cx =>
                {
                    var configuration = cx.Resolve<IConfiguration>();
                    var connStr = configuration.GetConnectionString(typeof(T).Name);
                    connStr.ThrowIfEmpty("Connection string");

                    var dboBuilder = new DbContextOptionsBuilder<T>()
                        .UseSqlServer(connStr, ox => { ox.MigrationsAssembly(assembly.FullName); });
                    //   .LogTo(Console.WriteLine,Microsoft.Extensions.Logging.LogLevel.Debug);

                    // _ = dboBuilder.ReplaceService<IStateManager, DbErrorResolver>();

                    return dboBuilder.Options;
                }).As<DbContextOptions<T>>()
                .SingleInstance();

            builder.RegisterType<T>()
                .WithParameter((p, c) => p.ParameterType == typeof(DbContextOptions) && p.Name == "options",
                    (p, c) => c.Resolve<DbContextOptions<T>>())
                .WithParameter((p, c) => p.ParameterType == typeof(IIdentityProvider),
                    (p, c) => c.Resolve<IIdentityProvider>()) // IIdentity Provider
                .AsSelf()
                .InstancePerLifetimeScope();

            builder.AddStores<T>(assembly);
        }

        private static void AddStores<T>(this ContainerBuilder builder, Assembly assembly) where T : DbContext
        {
            builder.RegisterType<DbDataStore<T>>()
                .As<IDataStore<T>>()
                .InstancePerLifetimeScope();
        }
    }
}