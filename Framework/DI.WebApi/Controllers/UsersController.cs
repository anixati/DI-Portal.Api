using DI.Domain.Users;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("users")]
    public class UsersController : GenericController<AppUser, UserViewModel>
    {
        public UsersController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
        }
    }
}