using System;
using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class FormField
    {
        public FormField(string key="")
        {
            Key = string.IsNullOrEmpty(key) ? Guid.NewGuid().ToString("N"):key.ToLower().Trim();
        }

        public string Key { get; }
        public LayoutType Layout { get; set; }
        public FormFieldType FieldType { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }
        public object Options { get; set; }
        public object Value { get; set; }
        public int? Width { get; set; } 
        public List<FormField> Fields { get; set; } = new();
    }
}