using System;
using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.Options;
using DI.Services.Core;
using DI.Services.Requests;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("option")]
    public class OptionsController : EntityController
    {
        public OptionsController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
        }

        #region Values

        [HttpPost("values")]
        public async Task<IActionResult> GetValues([FromBody] ListRequest request)
        {
            var result = await GetList<OptionSet, OptionValue>(request, o => o.OptionKeyId == request.KeyId);
            return result.ToResponse();
        }

        [HttpGet("value/{id}")]
        public async Task<IActionResult> GetValueById(long Id)
        {
            var result = await GetEntityById<OptionSet, OptionValue>(Id);
            return result.ToResponse();
        }

        [HttpPost("value")]
        public async Task<IActionResult> CreateValue([FromBody] OptionValue model)
        {
            var result = await Create<OptionSet, OptionValue>(model);
            return result.ToResponse();
        }


        [HttpPatch("value/{id}")]
        public async Task<IActionResult> PatchValue(long Id, [FromBody] JsonPatchDocument patchRequest)
        {
            var result = await PatchEntity<OptionSet>(Id, patchRequest);
            return result.ToResponse();
        }

        [HttpPost("value/change")]
        public async Task<IActionResult> ChangeValue([FromBody] SetStatusAction action)
        {
            var result = await ChangeStatus<OptionSet>(action);
            return result.ToResponse();
        }

        #endregion

        #region Keys

        [HttpGet("keys")]
        public async Task<IActionResult> GetKeys()
        {
            var result = await GetList<OptionKey, OptionModel>();
            return result.ToResponse();
        }

        [HttpGet("key/{id}")]
        public async Task<IActionResult> GetKeyById(long Id)
        {
            var result = await GetEntityById<OptionKey, OptionModel>(Id);
            return result.ToResponse();
        }

        [HttpPost("key")]
        public async Task<IActionResult> CreateKey([FromBody] OptionModel model)
        {
            var result = await Create<OptionKey, OptionModel>(model, e => { e.Code = e.Name.ToCode(); });
            return result.ToResponse();
        }


        [HttpPatch("key/{id}")]
        public async Task<IActionResult> PatchKey(long Id, [FromBody] JsonPatchDocument patchRequest)
        {
            var result = await PatchEntity<OptionKey>(Id, patchRequest);
            return result.ToResponse();
        }

        [HttpPost("key/change")]
        public async Task<IActionResult> ChangeKey([FromBody] SetStatusAction action)
        {
            var result = await ChangeStatus<OptionKey>(action);
            return result.ToResponse();
        }

        
        #endregion
    }
}