using System;
using System.Collections.Generic;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace DI.Forms.Requests
{
    public class FormActionRequest : IRequest<FormActionResult>
    {
        public ActionType Type { get; set; }
        public string SchemaKey { get; set; }
        public long? EntityId { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }

    public class EntityTypeRequest : IRequest<EntityTypeResponse>
    {
        public string SchemaKey { get; set; }
    }
    public class EntityTypeResponse
    {
        public Type EntityType { get; set; }
    }
}