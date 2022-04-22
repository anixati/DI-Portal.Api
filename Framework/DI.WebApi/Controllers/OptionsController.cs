using DI.Domain.Options;
using DI.Services.Core;
using DI.Services.Requests;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DI.Domain.Queries;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("option")]
    public class OptionsController : EntityController
    {
        public OptionsController(ILoggerFactory factory,IServiceContext context) : base(factory, context)
        {
        }

        [HttpGet("keys")]
        public async Task<IActionResult> GetKeys()
        {
            var result = await GetEntityList<OptionKey, OptionModel>(new ListRequest());
            return result.ToResponse();
        }

        [HttpGet("values/{id}")]
        public async Task<IActionResult> GetValues(long id)
        {
            var result= await ExecuteTask(async (x) =>
            {
                return await x.GetListByQry<OptionSet, OptionValue>(qb =>
                {
                    qb.Where(o => o.OptionKeyId == id);
                });
            });
            return result.ToResponse();
        }


    }
}