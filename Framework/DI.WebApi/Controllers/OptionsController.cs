using System;
using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.Options;
using DI.Services.Core;
using DI.Services.Requests;
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

        /// <summary>
        ///     CREATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("key")]
        public async Task<IActionResult> CreateKey([FromBody] OptionModel model)
        {
            var result = await Create<OptionKey, OptionModel>(model, e => { e.Code = e.Name.ToCode(); });
            return result.ToResponse();
        }


        /// <summary>
        ///     Patch
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="patchRequest"></param>
        /// <returns></returns>
        [HttpPatch("key/{id}")]
        public async Task<IActionResult> PatchKey(long Id, [FromBody] JsonPatchDocument patchRequest)
        {
            var result = await PatchEntity<OptionKey>(Id, patchRequest);
            return result.ToResponse();
        }

        /// <summary>
        ///     UPDATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("key/change")]
        public async Task<IActionResult> ChangeKey([FromBody] SetStatusAction action)
        {
            var result = await ChangeStatus<OptionKey>(action);
            return result.ToResponse();
        }


        //status 

        #endregion
    }
}