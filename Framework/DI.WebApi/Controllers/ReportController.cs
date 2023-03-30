using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DI.Reports;
using DI.Services.Core;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("reports")]
    [Authorize]
    public class ReportController : ServiceController
    {
        public ReportController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory,
            serviceContext)
        {
        }

        [HttpGet]
        public async Task<IActionResult> GetReports()
        {
            var result = await ExecuteTask(async x => await x.Send(new ReportListRequest()));
            return result.ToResponse();
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetCreateSchema([Required] int id)
        {
            var result = await ExecuteTask(async x => await x.Send(new ReportListRequest {Id = id}));
            return result.ToResponse();
        }
    }
}