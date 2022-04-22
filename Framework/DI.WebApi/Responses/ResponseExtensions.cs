using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Core;
using DI.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DI.WebApi.Responses
{
    public static class ResponseExtensions
    {
        public static async Task WriteResponseAsync(this HttpContext context, string responseStr, int statusCode)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;
            await context.Response.WriteAsync(responseStr);
        }


        public static IActionResult ToResponse(this IApiResponse response)
        {
            try
            {
                switch (response.Reason)
                {
                    case ResponseCode.Default:
                        return new OkObjectResult(response.Result);
                    case ResponseCode.Created:
                        return new CreatedResult("", response.Result);
                    case ResponseCode.Deleted:
                        break;
                    case ResponseCode.NotFound:
                        return new NotFoundObjectResult(response.Messages);
                    case ResponseCode.BadRequest:
                        return new ObjectResult(response.Messages) { StatusCode = StatusCodes.Status400BadRequest };
                    case ResponseCode.UnAuthorized:
                        return new UnauthorizedObjectResult(response.Messages);
                    case ResponseCode.ServerError:
                        return new ObjectResult(response.Messages) { StatusCode = StatusCodes.Status500InternalServerError };
                    case ResponseCode.TimedOut:
                        return new ObjectResult(response.Result) { StatusCode = StatusCodes.Status500InternalServerError };
                    case ResponseCode.Duplicate:
                        return new ObjectResult(response.Messages) { StatusCode = StatusCodes.Status400BadRequest };
                    case ResponseCode.Updated:
                    case ResponseCode.Disabled:
                    case ResponseCode.Enabled:
                    case ResponseCode.Locked:
                    case ResponseCode.Unlocked:
                    case ResponseCode.StateChanged:
                    case ResponseCode.Superseded:
                    default:
                        throw new NotImplementedException();
                }
            }
            catch (Exception)
            {
                return new StatusCodeResult(500);
            }

            return new EmptyResult();
        }
    }
}
