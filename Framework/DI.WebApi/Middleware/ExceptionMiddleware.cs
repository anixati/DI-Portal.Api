using System;
using System.Net;
using System.Threading.Tasks;
using DI.Core;
using DI.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace DI.WebApi.Middleware
{
    public class ExceptionMiddleware : ComponentBase
    {
        private const int InternalError = (int) HttpStatusCode.InternalServerError;
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next, ILoggerFactory loggerFactory) : base(loggerFactory)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {
                await _next(httpContext);
            }
            catch (InvalidRequestException inx)
            {
                await httpContext.WriteResponseAsync(GetStr(inx), inx.ErrorCode);
            }
            catch (Exception ex)
            {
                Log.LogCritical(ex, "unhandled error");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private static string GetStr(ServiceBaseException exception, int code = 400)
        {
            return JsonConvert.SerializeObject(new
            {
                Status = code,
                exception.Message
            });
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var responseStr = JsonConvert.SerializeObject(new
            {
                Status = 500,
                exception.Message
            });
            await context.WriteResponseAsync(responseStr, InternalError);
        }
    }
}