using System;

namespace DI.Forms.Types
{
    public class FormField
    {
        public FormField()
        {
            Key = Guid.NewGuid().ToString("N");
        }

        public string Key { get; }
        public FormFieldType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public object Options { get; set; }
        public object Value { get; set; }
    }
}