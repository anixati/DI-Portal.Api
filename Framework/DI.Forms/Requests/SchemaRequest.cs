using MediatR;

namespace DI.Forms.Requests
{
    public enum ActionType
    {
        Create=0,
        View,
        Manage,
        Dialog
    }

    public class FormSchemaRequest : IRequest<FormSchemaResponse>
    {
        public long? EntityId { get; set; }
        public ActionType Type { get; set; }
        public string Name { get; set; }
    }
}