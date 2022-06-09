using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Domain.Requests;
using DI.Forms.Requests;
using DI.Services.Core;
using DI.Site;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("site")]
    public class SiteController : ServiceController
    {
        public SiteController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }

        [HttpGet("")]
        public async Task<IActionResult> GetSiteMap()
        {
            var result = await ExecuteTask(async x => await x.Send(new SiteMapRequest() ));
            return result.ToResponse();
        }
    }
}