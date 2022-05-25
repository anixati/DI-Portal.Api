using MediatR;

namespace DI.Forms.Requests
{
    public class FormSchemaRequest : IRequest<FormSchemaResponse>
    {
        public string Name { get; set; }
    }
}