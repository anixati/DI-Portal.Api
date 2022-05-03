using System;
using DI.Domain.Options;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("options")]
    public class OptionsController : GenericController<OptionKey, OptionModel>
    {
        public OptionsController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
            this.OnCreateAction = (e, v) =>
            {
                e.Code = v.Name.ToCode();
            };
        }
    }
}