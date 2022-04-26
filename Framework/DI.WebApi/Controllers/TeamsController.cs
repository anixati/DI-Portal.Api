using DI.Domain.Users;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("teams")]
    public class TeamsController : GenericController<AppTeam, RoleViewModel>
    {
        public TeamsController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
           // this.GetItemFilter = r => r.Locked == true;
        }
    }
}