using System;
using DI.Forms.Core;
using DI.Forms.Types;

namespace DI.Forms.Builders
{
    public class FormState : IFormState
    {
        private readonly FormSchema _schema;

        public FormState(string name)
        {
            _schema = new FormSchema(name);
        }

        public FormSchema Schema =>_schema;

        public FormSchema Build()
        {

            return _schema;
        }
    }
}