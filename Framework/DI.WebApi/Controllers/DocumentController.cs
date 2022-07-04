using System.Threading.Tasks;
using DI.Domain.Activities;
using DI.Domain.Requests;
using DI.Services.Core;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("documents")]
    [Authorize]
    public class DocumentController :  GenericController<Activity, ActivityViewModel>
    {
        public DocumentController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }
        
    }
}