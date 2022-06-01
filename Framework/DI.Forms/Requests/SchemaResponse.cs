using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Requests
{
    public class FormSchemaResponse
    {
        public FormEntity Entity { get; set; }
        public object InitialValues  { get; set; }
        public FormSchema Schema { get; set; }
    }
}