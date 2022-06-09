using System;
using System.Linq;
using DI.Domain.Core;
using DI.Extensions;
using DI.Response;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace DI.Domain.Requests
{
    public class PatchEntityRequest<T> : PatchRequest, IRequest<DomainResponse> where T : class, IEntity
    {
        public PatchEntityRequest(long id, JsonPatchDocument data) : base(id, data)
        {
        }
    }
}