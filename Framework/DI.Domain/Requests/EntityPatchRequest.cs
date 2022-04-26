﻿using System;
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
        public PatchEntityRequest(long id, JsonPatchDocument request) : base(id, request)
        {
        }

        public void Apply(T entity)
        {
            var invalidList = entity.GetInvalidPatchPaths();
            if (invalidList.Any())
                if (Request.Operations.Any(operation => invalidList.Contains(operation.path)))
                    throw new UnauthorizedAccessException();
            Request.ApplyTo(entity);
        }
    }
}