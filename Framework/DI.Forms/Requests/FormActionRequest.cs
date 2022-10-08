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

    public class DialogActionRequest : IRequest<DialogActionResponse>
    {
        public string SchemaKey { get; set; }
        public long EntityId { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }

    public class DialogActionResponse
    {
        public bool Failed { get; set; }
        public string Result { get; set; }
    
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