using System.Collections.Generic;
using MediatR;

namespace DI.Forms.Requests
{
    public class FormActionRequest : IRequest<FormActionResult>
    {
        public string SchemaKey { get; set; }
        public long? EntityId { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }
}