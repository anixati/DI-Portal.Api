using System;
using System.Threading.Tasks;
using DI.Core;
using DI.Response;
using DI.Services.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Core
{
    [ApiController]
    public abstract class ServiceController : ControllerBase
    {
        private readonly IServiceContext _serviceContext;

        protected ServiceController(ILoggerFactory loggerFactory, IServiceContext serviceContext)
        {
            Log = loggerFactory.CreateLogger(GetType().Name);
            _serviceContext = serviceContext;
        }

        protected ILogger Log { get; }

        protected void Trace(string msg)
        {
            Log.LogDebug(msg);
        }

        protected int GetVersion()
        {
            if (!RouteData.Values.TryGetValue("version", out var verStr)) return 1;
            if (verStr != null && int.TryParse(verStr.ToString(), out var version))
                return version;
            return 1;
        }

        protected async Task<IApiResponse> ExecuteTask(Func<IServiceContext, Task<object>> executor)
        {
            return await ExecuteAsync(async () => await executor(_serviceContext));
        }

        protected async Task<IApiResponse> ExecuteAsync(Func<Task<object>> executor)
        {
            try
            {
                var result = await executor();
                result.ThrowIfNull("response is null ");
                return ApiResponse.Success(result);
            }
            catch (Exception ex)
            {
                Log.LogError(ex, "Execute Async");
                return ApiResponse.Error(ex);
            }
        }
    }
}