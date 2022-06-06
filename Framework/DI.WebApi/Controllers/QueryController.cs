using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Di.Qry.Core;
using Di.Qry.Requests;
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
    [Route("qry")]
    public class QueryController : ServiceController
    {
        public QueryController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory,
            serviceContext)
        {
        }

        [HttpGet("schemas")]
        public async Task<IActionResult> GetSchemas()
        {
            var result = await ExecuteTask(async x => await x.Send(new SchemaListRequest()));
            return result.ToResponse();
        }

        [HttpGet("schema/{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var result = await ExecuteTask(async x => await x.Send(new SchemaRequest {Name = name}));
            return result.ToResponse();
        }

        [HttpPost("schema/{name}")]
        public virtual async Task<IActionResult> GetItems([Required] string name, [FromBody] QueryRequest request)
        {
            var qryReq = new QryRequest(name,request.EntityId)
            {
                SearchStr = request.SearchStr,
                PageInfo = new PageInfo(request.Index.GetValueOrDefault(), request.Size.GetValueOrDefault()),
                SortInfos = request.SortBy.Select(x => new SortInfo(x.Id, x.Desc)).ToList(),
                Filter =request.Filter 
            };
            var result = await ExecuteTask(async x => await x.Send(qryReq));
            return result.ToResponse();
        }
    }
}