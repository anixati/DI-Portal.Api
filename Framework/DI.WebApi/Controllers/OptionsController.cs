using System;
using DI.Domain.Options;
using DI.Services.Core;
using DI.Services.Requests;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;
using DI.Actions;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("option")]
    public class OptionsController : EntityController
    {
        public OptionsController(ILoggerFactory factory,IServiceContext context) : base(factory, context)
        {
        }

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
        /// CREATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("keys")]
        public async Task<IActionResult> CreateKey([FromBody] OptionModel model)
        {
            var result = await Create<OptionKey, OptionModel>(model, e =>
            {
                e.Code = e.Name.ToCode();
            });
            return result.ToResponse();
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPut("keys")]
        public async Task<IActionResult> UpdateKey([FromBody] OptionModel model)
        {
            var result = await Update<OptionKey, OptionModel>(model);
            return result.ToResponse();
        }

        /// <summary>
        /// UPDATE
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("keys/change")]
        public async Task<IActionResult> ChangeKey([FromBody] SetStatusAction action)
        {
            var result = await ChangeStatus<OptionKey>(action);
            return result.ToResponse();
        }

        
        //status 

        #endregion

        #region Values

        [HttpPost("values")]
        public async Task<IActionResult> GetValues([FromBody] ListRequest request)
        {

            var result = await GetList<OptionSet, OptionValue>(request,o => o.OptionKeyId== request.KeyId);
            return result.ToResponse();
        } 


        #endregion


    }
}