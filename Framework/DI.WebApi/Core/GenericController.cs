using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Services.Core;
using DI.Services.Requests;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Core
{
    public abstract class GenericController<T,TK> : EntityController where T : class, IEntity where TK : class, IViewModel
    {
        protected Expression<Func<T, bool>> GetItemFilter = null;
        protected Action<T,TK> OnCreateAction = null;
        protected GenericController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }
        [HttpPost]
        public virtual async  Task<IActionResult> GetItems([FromBody] ListRequest request)
        {
            var result = await GetList<T, TK>(request, GetItemFilter);
            return result.ToResponse();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetIItemById(long Id)
        {
            var result = await GetEntityById<T, TK>(Id);
            return result.ToResponse();
        }

        [HttpPost("create")]
        public async Task<IActionResult> CreateItem([FromBody] TK model)
        {
            var result = await Create<T, TK>(model, e=> OnCreateAction?.Invoke(e,model));
            return result.ToResponse();
        }


        [HttpPatch("{id}")]
        public async Task<IActionResult> PatchItem(long Id, [FromBody] JsonPatchDocument patchRequest)
        {
            var result = await PatchEntity<T>(Id, patchRequest);
            return result.ToResponse();
        }

        [HttpPost("change")]
        public async Task<IActionResult> ChangeItem([FromBody] SetStatusAction action)
        {
            var result = await ChangeStatus<T>(action);
            return result.ToResponse();
        }


       
    }
}