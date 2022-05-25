using System;
using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Builders
{
    public class FormState : IFormState
    {
        private FormSchema _schema;

        public FormState()
        {
            _schema = new FormSchema();
        }

        public FormSchema Build()
        {
            throw new NotImplementedException();
        }
    }
}