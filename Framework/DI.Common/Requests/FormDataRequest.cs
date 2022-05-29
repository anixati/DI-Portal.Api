using System.Collections.Generic;

namespace DI.Requests
{
    public class FormDataRequest
    {
        public string Schema { get; set; }
        public long? EntityId { get; set; }
        public Dictionary<string, object> Data { get; set; } = new();
    }
}