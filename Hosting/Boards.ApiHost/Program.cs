using System;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Boards.Infrastructure.Seeding;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace Boards.ApiHost
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            SetupLogger();
            try
            {
                Log.Information("Starting");
                var hostBuilder = CreateHostBuilder(args);
                var host = hostBuilder.Build();
                Log.Information("Started");
               // await RunHostStartUp(host);
                await host.RunAsync();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Failed to start");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static async Task RunHostStartUp(IHost host)
        {
            using var scope = host.Services.CreateScope();
            var services = scope.ServiceProvider;
            await BoardsData.Run(services);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static void SetupLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console()
                .CreateLogger();
        }
    }
}