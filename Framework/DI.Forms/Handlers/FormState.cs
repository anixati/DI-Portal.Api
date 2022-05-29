using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Handlers
{
    public class FormState : IFormState
    {
        public FormState(string name)
        {
            Schema = new FormSchema(name);
        }

        public FormSchema Schema { get; }

        public FormSchema Build()
        {
            return Schema;
        }
    }
}