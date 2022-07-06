using DI.Domain.Activities;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("documents")]
   // [Authorize]
    public class DocumentController : GenericController<Activity, ActivityViewModel>
    {
        public DocumentController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
        }
    }
}