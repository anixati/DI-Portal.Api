using MediatR;

namespace DI.Forms.Requests
{
    public class DialogSchemaRequest : IRequest<DialogSchemaResponse>
    {
        public long? EntityId { get; set; }
        public string Name { get; set; }
    }


}