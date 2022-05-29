using System.Threading.Tasks;
using DI.Forms.Requests;
using DI.Requests;
using DI.Services.Core;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [AllowAnonymous]
    [ApiVersionNeutral]
    [Route("forms")]
    public class FormsController : ServiceController
    {
        public FormsController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory,
            serviceContext)
        {
        }

        [HttpGet("schemas")]
        public async Task<IActionResult> GetSchemas()
        {
            var result = await ExecuteTask(async x => await x.Send(new FormsListRequest()));
            return result.ToResponse();
        }

        [HttpGet("schema/{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var result = await ExecuteTask(async x => await x.Send(new FormSchemaRequest {Name = name}));
            return result.ToResponse();
        }

        [HttpPost("process")]
        public virtual async Task<IActionResult> GetItems([FromBody] FormDataRequest request)
        {
            var result = await ExecuteTask(async x => await x.Send(new FormActionRequest
            {
                SchemaKey = request.Schema,
                EntityId = request.EntityId,
                Data = request.Data
            }));
            return result.ToResponse();
        }
    }
}