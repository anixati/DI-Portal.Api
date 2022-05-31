using DI.Forms.Types;

namespace DI.Forms.Requests
{
    public class FormSchemaResponse
    {
        public object InitialValues  { get; set; }
        public FormSchema Schema { get; set; }
    }
}