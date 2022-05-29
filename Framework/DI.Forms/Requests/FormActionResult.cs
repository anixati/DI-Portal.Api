using System.Collections.Generic;

namespace DI.Forms.Requests
{
    public class ValResult
    {
        public string Message { get; set; }
        public string Key { get; set; }
    }

    public class FormActionResult
    {
        public long EntityId { get; set; }
        public List<ValResult> ValResults { get; set; } = new();
        public string RouteKey { get; set; }
    }
}