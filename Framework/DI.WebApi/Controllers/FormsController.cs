﻿using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using DI.Actions;
using DI.Domain.Config;
using DI.Domain.Requests;
using DI.Forms.Requests;
using DI.Requests;
using DI.Response;
using DI.Services.Core;
using DI.WebApi.Core;
using DI.WebApi.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    [ApiVersionNeutral]
    [Route("forms")]
    public class FormsController : EntityController
    {
        public FormsController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory,
            serviceContext)
        {
        }

        [HttpGet("schemas")]
        public async Task<IActionResult> GetSchemas()
        {
            var result = await ExecuteTask(async x => await x.Send(new FormsListRequest()));
            return result.ToResponse();
        }

        [HttpGet("create/{name}")]
        public async Task<IActionResult> GetSchema(string name)
        {
            var result = await ExecuteTask(async x => await x.Send(new FormSchemaRequest {Name = name}));
            return result.ToResponse();
        }


        [HttpGet("view/{name}/{id}")]
        public async Task<IActionResult> GetSchema([Required] string name, [Required] long id)
        {
            var result = await ExecuteTask(async x => await x.Send(new FormSchemaRequest {Name = name, EntityId = id}));
            return result.ToResponse();
        }

        [HttpPost("create")]
        public virtual async Task<IActionResult> SubmitCreateForm([FromBody] FormDataRequest request)
        {
            var result = await ExecuteTask(async x => await x.Send(new FormActionRequest
            {
                SchemaKey = request.Schema,
                EntityId = request.EntityId,
                Data = request.Data
            }));
            return result.ToResponse();
        }

        [HttpPatch("update/{name}/{id}")]
        public virtual async Task<IActionResult> PatchItem([Required] string name, [Required] long id,
            [FromBody] JsonPatchDocument patchRequest)
        {
            var etResp = await Send(new EntityTypeRequest
            {
                SchemaKey = name
            });
            var rx = CreatePatchRequest(etResp.EntityType, id, patchRequest);
            var result = await ExecuteTask(async x => await x.PatchEntity(rx));
            return result.ToResponse();
        }

        private dynamic CreatePatchRequest(Type entityType, long id, JsonPatchDocument patchRequest)
        {
            var genericType = typeof(PatchEntityRequest<>);
            var reqType = genericType.MakeGenericType(entityType);
            return Activator.CreateInstance(reqType, new object[] {id, patchRequest});
        }


        [HttpPost("change/{name}")]
        public virtual async Task<IActionResult> SubmitChangeForm([Required] string name, [FromBody] SetStatusAction action)
        {
            var etResp = await Send(new EntityTypeRequest
            {
                SchemaKey = name
            });
            var rx = CreateStateRequest(etResp.EntityType,action.Action,action.Id,action.Reason);
            var result = await ExecuteTask(async x => await x.ChangeStatus(rx));
            return result.ToResponse();
        }

        private dynamic CreateStateRequest(Type entityType, SetStatusAction.ActionType action, long id, string reason)
        {
            var genericType = typeof(Entity.ChangeState<>);
            var reqType = genericType.MakeGenericType(entityType);
            return Activator.CreateInstance(reqType, new object[] {action, id, reason});
        }
    }
}