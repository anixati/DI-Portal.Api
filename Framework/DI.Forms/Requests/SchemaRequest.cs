using MediatR;

namespace DI.Forms.Requests
{
    public class FormSchemaRequest : IRequest<FormSchemaResponse>
    {
        public long? EntityId { get; set; }
        public string Name { get; set; }
    }
}