using System.Collections.Generic;
using Di.Qry.Core;

namespace Di.Qry.Requests
{
    public class SchemaListResponse
    {
        public List<SchemaName> Schemas { get; set; }
    }
}