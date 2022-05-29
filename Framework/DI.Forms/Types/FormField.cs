using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.Forms.Types
{
    public class FormField
    {
        public FormField(string key = "")
        {
            Key = string.IsNullOrEmpty(key) ? Guid.NewGuid().ToString("N") : key.Trim();
            Width = 70;
        }

        public string Key { get; }
        public LayoutType Layout { get; set; }
        public FormFieldType FieldType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public object Options { get; set; }
        public object Value { get; set; }
        public int Width { get; set; }

        public bool Required
        {
            get
            {
                var x = Rules.FirstOrDefault(x => x.Rule == ValRule.RuleType.Required);
                return x != null;
            }
        }

        public string ValType =>
            FieldType switch
            {
                FormFieldType.Number => "number",
                _ => "string"
            };

        public List<ValRule> Rules { get; set; } = new();
        public List<FormField> Fields { get; set; } = new();
    }
}