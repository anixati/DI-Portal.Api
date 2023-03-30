using DI.Forms.Types;

namespace DI.Forms.Requests
{
    public class DialogSchemaResponse
    {
        public object InitialValues { get; set; }
        public FormSchema Schema { get; set; }
    }
}