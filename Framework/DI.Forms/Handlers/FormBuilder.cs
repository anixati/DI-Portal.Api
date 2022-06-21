using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Handlers
{
    public abstract class FormBuilder : IFormBuilder
    {
        public abstract string FormName { get; }
        public IFormState Create()
        {
            var fs = new FormState(FormName, FormType);
            CreateSchema(fs.Schema);
            return fs;
        }
        protected virtual void CreateSchema(FormSchema fsSchema)
        {
        }

        protected virtual FormType FormType => FormType.Wizard;
    }
}