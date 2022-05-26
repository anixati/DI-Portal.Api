using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class FormSchema
    {
        public FormSchema(string name)
        {
            Name = name;
        }

        public string Name{ get;  }
        public List<FormField> Fields { get; set; } = new();
    }
}