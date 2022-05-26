using System;
using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Builders
{
    public abstract class FormBuilder : IFormBuilder
    {
        public abstract string FormName { get; }

        public IFormState Create()
        {
            var fs = new FormState(FormName);
            CreateSchema(fs.Schema);
            return fs;
        }

        protected virtual void CreateSchema(FormSchema fsSchema)
        {
        }
    }
}