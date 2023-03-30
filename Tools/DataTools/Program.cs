using System;
using System.IO;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using Boards.Domain;
using Boards.Infrastructure;
using Boards.Services;
using DI;
using DI.Security;
using DI.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;

namespace DataTools
{
    internal class Program
    {
        private static IConfiguration config;

        private static async Task Main(string[] args)
        {
            config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(config)
                .Enrich.FromLogContext()
                .CreateLogger();
            await Host.CreateDefaultBuilder(args)
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices(ConfigureServices)
                .ConfigureContainer<ContainerBuilder>(ConfigureContainer)
                .UseSerilog()
                .RunConsoleAsync();
            Console.ReadKey();
        }

        private static void ConfigureServices(IServiceCollection services)
        {
            services.AddSingleton<IConfigurationRoot>((IConfigurationRoot) config);

            services.AddHostedService<ImportCrmDataService>();
        }

        private static void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterType<MigUserProvider>().As<IIdentityProvider>();
            //-- Framework Modules ---
            builder.RegisterModule(new InfrastructureModule());
            builder.RegisterModule(new ServiceModule());


            //-- Domain Modules---
            builder.RegisterModule(new BoardsDomainModule());

            //-- Infrastructure Modules---
            builder.RegisterModule(new BoardsInfraModule());

            //-- Application Modules---
            builder.RegisterModule(new BoardsServiceModule());
        }
    }
}