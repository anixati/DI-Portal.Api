using System.Linq;
using Autofac;
using DI.WebApi;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;

namespace Boards.ApiHost
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.InitialiseHost();

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    policy =>
                    {
                        policy.AllowAnyOrigin()
                            .AllowAnyMethod()
                            .AllowAnyHeader();
                    });
            });

            services.AddApiControllers(x => x.RoutePrefix = "brds")
                //.AddJsonOptions(options =>
                //{
                //    options.JsonSerializerOptions.PropertyNameCaseInsensitive = true;
                //    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                //    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
                //}) 
                .AddNewtonsoftJson()
                .AddEndpoints(Modules.GetControllers);
            //.AddFeatures(Modules.GetFeatures);

            services.AddSwaggerGen(c =>
            {
                c.ResolveConflictingActions(x => x.First());
                c.SwaggerDoc("v1", new OpenApiInfo {Title = "Boards.ApiHost", Version = "v1"});
            });
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.AddModules();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Boards.ApiHost v1"));
            }

            app.AddEndPoints();
        }
    }
}