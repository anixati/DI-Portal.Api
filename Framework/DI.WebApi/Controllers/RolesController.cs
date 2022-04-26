using System;
using DI.Domain.Users;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("roles")]
    public class RolesController : GenericController<AppRole, RoleViewModel>
    {
        public RolesController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
           // this.GetItemFilter = r => r.Locked == true;
           this.OnCreateAction = (e, v) =>
           {
               e.Code = v.Name.ToCode();
           };
        }
    }
}