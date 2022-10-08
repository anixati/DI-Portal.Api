using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class FormSchema
    {
        public FormSchema(string name, FormType formType = FormType.Wizard)
        {
            Name = name;
            FormType = formType;
        }

        public string Name { get; }
        public FormType FormType { get; }

        public List<FormAction> Actions { get; set; } = new();
        public List<SelectItem> Options { get; set; } 
        public List<FormField> Fields { get; set; } = new();
    }


    public enum FormType
    {
      Default=0,
      Wizard,
      MultiSelect,
      Dialog
    }
}