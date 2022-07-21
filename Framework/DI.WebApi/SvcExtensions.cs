using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using DI.Reports;
using DI.WebApi.Middleware;
using DI.WebApi.Routes;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace DI.WebApi
{
    public static class SvcExtensions
    {
        public static async Task WriteResponseAsync(this HttpContext context, string responseStr, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(responseStr);
        }

        public static void InitialiseHost(this IServiceCollection services)
        {
            services.AddOptions();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }

        #region options

        public static void AddReportConfig(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ReportConfig>(configuration.GetSection("reportConfig"));
        }


        #endregion

        public static void AddTokenAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        //ValidateIssuerSigningKey = true,
                        ValidIssuer = configuration["JWT:Issuer"],
                        ValidAudience = configuration["JWT:Audience"],
                    };


                    options.Authority = configuration["JWT:Issuer"];
                    options.RequireHttpsMetadata = false;
                    // allow self-signed SSL certs
                    options.BackchannelHttpHandler = new HttpClientHandler { ServerCertificateCustomValidationCallback = delegate { return true; } };
                    // the scope id of this api
                    options.Audience = configuration["JWT:Audience"];
                });
        }



        public static void SetupExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
        }

        public static IMvcBuilder AddApiControllers(this IServiceCollection services, Action<ApiSettings> setup = null)
        {
            return services.AddApiControllers(() => ApiSettings.Create(setup));
        }

        public static IMvcBuilder AddApiControllers(this IServiceCollection services, Func<ApiSettings> getConfigFunc)
        {
            var config = getConfigFunc();

            services.AddApiVersion();

            return services.AddControllers(mo =>
                {
                    //mo.ModelBinderProviders.Insert(0, new EntityBinderProvider());
                    mo.UseRoutePrefix(config.RoutePrefix + "/v{version:apiVersion}");
                    mo.AddConventions();
                    
                })
                
                .AddFluentValidation(fv => config.Validation(fv));
        }

        public static void AddApiVersion(this IServiceCollection services, int majorVersion = 1, int minorVersion = 0)
        {
            services.AddApiVersioning(config =>
            {
                config.DefaultApiVersion = new ApiVersion(1, 0);
                config.AssumeDefaultVersionWhenUnspecified = true;
                config.ReportApiVersions = true;
            });
        }

        public static void AddConventions(this MvcOptions opts)
        {
            //table Routes
            //opts.Conventions.Add(new EntityRouteConvention());

            //add auth Policies
            // opts.Conventions.Add(new EntityControllerConvention());
        }

        public static IMvcBuilder AddEndpoints(this IMvcBuilder builder,
            Func<IEnumerable<AssemblyPart>> assemblyPartsFunc)
        {
            builder.ConfigureApplicationPartManager(apm =>
            {
                apm.ApplicationParts.Add(new AssemblyPart(typeof(SvcExtensions).Assembly));
                if (assemblyPartsFunc == null) return;
                foreach (var part in assemblyPartsFunc())
                    apm.ApplicationParts.Add(part);
            });
            return builder;
        }

        public static IMvcBuilder AddFeatures(this IMvcBuilder builder,
            Func<IEnumerable<IApplicationFeatureProvider>> featuresFunc)
        {
            builder.ConfigureApplicationPartManager(apm =>
            {
                if (featuresFunc == null) return;
                foreach (var feature in featuresFunc())
                    apm.FeatureProviders.Add(feature);
            });
            return builder;
        }


        public static void AddEndPoints(this IApplicationBuilder appBuilder,
            Action<IEndpointRouteBuilder> mapRouteAction = null)
        {
            appBuilder.SetupExceptionMiddleware();
            appBuilder.UseHttpsRedirection();
            appBuilder.UseAuthentication();
            appBuilder.UseRouting();
            appBuilder.UseCors();

            appBuilder.UseAuthorization();
            appBuilder.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                mapRouteAction?.Invoke(endpoints);
            });
        }
    }
}