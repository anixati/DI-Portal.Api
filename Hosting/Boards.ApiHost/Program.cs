using System;
using System.IO;
using System.Net;
using System.Reflection;
using System.Threading.Tasks;
using Autofac.Extensions.DependencyInjection;
using Boards.Services;
using DI.Jobs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;

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
            await Task.Delay(0);
            //using var scope = host.Services.CreateScope();
            //var services = scope.ServiceProvider;
            //await BoardsData.Run(services);
        }

        public static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .UseSerilog()
                .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                .ConfigureServices((hc, services) =>
                {
                    var jobAssembly = typeof(BoardsServiceModule).Assembly;
                    services.SetupJobs(hc.Configuration, jobAssembly.GetJobs);
                })
                .ConfigureAppConfiguration((hc, config) =>
                {
                    config.AddJsonFile("reportConfig.json", optional: false, reloadOnChange: true);
                })
                .ConfigureWebHostDefaults(webBuilder =>
                {
#if DEBUG
                    webBuilder.ConfigureKestrel(options =>
                    {
                        options.Listen(IPAddress.Any, 5010, listenOptions =>
                        {
                            listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
                            listenOptions.UseHttps(@"C:\Temp\Certs\akdev.pfx", "Welcome1");
                        });
                    });
#endif


                    webBuilder.UseStartup<Startup>();
                });
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
                .Enrich.FromLogContext()
                .CreateLogger();
        }
    }
}