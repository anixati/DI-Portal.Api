using System.ComponentModel.DataAnnotations;
using System.Linq;
using Di.Qry.Requests;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using Di.Qry.Core;
using DI.Requests;
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
            var result = await ExecuteTask(async x => await x.Send(new SchemaListRequest()));
            return result.ToResponse();
        }
        [HttpGet("schema/{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var result = await ExecuteTask(async x => await x.Send(new SchemaRequest(){ Name = name}));
            return result.ToResponse();
        }

        [HttpPost("schema/{name}")]
        public virtual async Task<IActionResult> GetItems([Required]string name,[FromBody] QueryRequest request)
        {
            var qryReq = new QryRequest(name)
            {
                SearchStr = request.SearchStr,
                PageInfo = new PageInfo(request.Index.GetValueOrDefault(),request.Size.GetValueOrDefault()),
                SortInfos = request.SortBy.Select(x=> new SortInfo(x.Id,x.Desc)).ToList()
            };
            var result = await ExecuteTask(async x => await x.Send(qryReq));
            return result.ToResponse();
        }

    }
}