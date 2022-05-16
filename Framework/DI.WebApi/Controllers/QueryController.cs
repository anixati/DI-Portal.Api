using Di.Qry.Requests;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DI.WebApi.Responses;

namespace DI.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiVersionNeutral]
    [Route("qry")]
    public class QueryController : ServiceController
    {

        public QueryController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }

        [HttpGet("schemas")]
        public async Task<IActionResult> GetSchemas()
        {
            var result = await ExecuteTask(async x => await x.Send(new ConfigRequest()));
            return result.ToResponse();
        }
        [HttpGet("schema/{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var result = await ExecuteTask(async x => await x.Send(new ConfigRequest{ SchemaName = name}));
            return result.ToResponse();
        }

    }
}