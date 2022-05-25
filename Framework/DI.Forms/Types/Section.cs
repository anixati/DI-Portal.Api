using System;
using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class Section
    {
        public Section()
        {
            Key = Guid.NewGuid().ToString("N");
        }

        public string Key { get; }
        public LayoutType Type { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool Disabled { get; set; }

        public List<FormField> Fields { get; set; } = new();
    }
}