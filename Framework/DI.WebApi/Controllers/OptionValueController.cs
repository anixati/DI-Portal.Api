using DI.Domain.Options;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("optval")]
    [Authorize]
    public class OptionValueController : GenericController<OptionSet, OptionValue>
    {
        public OptionValueController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
            SetFilter = r => { ItemFilter = e => e.OptionKeyId == r.KeyId; };
        }
    }
}