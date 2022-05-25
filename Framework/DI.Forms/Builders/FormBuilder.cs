using System;
using DI.Forms.Core;

namespace DI.Forms.Builders
{
    public abstract class FormBuilder : IFormBuilder
    {
        public abstract string FormName { get; }

        public IFormState Create()
        {
            throw new NotImplementedException();
        }
    }
}