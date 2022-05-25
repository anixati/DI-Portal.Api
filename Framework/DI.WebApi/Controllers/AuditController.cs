using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.App;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("audits")]
    public class AuditController : GenericController<AuditHistory, AuditModel>
    {
        public AuditController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
        }

        public override async Task<IActionResult> CreateItem([FromBody] AuditModel model)
        {
            await Task.Delay(0);
            return BadRequest();
        }

        public override async Task<IActionResult> PatchItem(long Id, [FromBody] JsonPatchDocument patchRequest)
        {
            await Task.Delay(0);
            return BadRequest();
        }


        public override async Task<IActionResult> ChangeItem([FromBody] SetStatusAction action)
        {
            await Task.Delay(0);
            return BadRequest();
        }
    }
}