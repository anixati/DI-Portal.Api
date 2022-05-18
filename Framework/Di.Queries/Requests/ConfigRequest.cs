using MediatR;

namespace Di.Qry.Requests
{
    public class SchemaRequest : IRequest<SchemaResponse>
    {
        public string Name { get; set; }
    }


    public class SchemaListRequest : IRequest<SchemaListResponse>
    {
    }
}