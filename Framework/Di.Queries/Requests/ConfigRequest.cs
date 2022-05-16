using MediatR;

namespace Di.Qry.Requests
{
    public class ConfigRequest : IRequest<ConfigResponse>
    {
        public string SchemaName { get; set; }
    }
}