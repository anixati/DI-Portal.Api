using System;
using System.IO;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Boards.Infrastructure.Design;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
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
                await RunHostStartUp(host);
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
                .ConfigureAppConfiguration((hc, config) =>
                {
                    config.AddJsonFile("reportConfig.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder => { webBuilder.UseStartup<Startup>(); });
        }

        public static void SetupLogger()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            var _config = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile("logSettings.json")
                
                .Build();

            Log.Logger = new LoggerConfiguration()
                .ReadFrom.Configuration(_config)
                //.MinimumLevel.Debug()
                //.MinimumLevel.Override("Microsoft", LogEventLevel.Information)
                .Enrich.FromLogContext()
               // .WriteTo.Console()
                .CreateLogger();
        }
    }
}