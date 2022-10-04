﻿using System.ComponentModel.DataAnnotations;
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
    [Route("dashboard")]
    [Authorize]
    public class DashboardController : ServiceController
    {
        public DashboardController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetData([Required] int id)
        {
            var result = await ExecuteTask(async x => await x.Send(new DashboardDataRequest { DashboardId= id}));
            return result.ToResponse();
        }

    }

}