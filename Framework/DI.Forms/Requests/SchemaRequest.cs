using MediatR;

namespace DI.Forms.Requests
{
    public enum SchemaRequestType
    {
        Create=0,
        View
    }

    public class FormSchemaRequest : IRequest<FormSchemaResponse>
    {
        public long? EntityId { get; set; }
        public SchemaRequestType RequestType { get; set; }
        public string Name { get; set; }
    }
}