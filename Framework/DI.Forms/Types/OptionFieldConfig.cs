using System.Collections.Generic;

namespace DI.Forms.Types
{
    public class OptionFieldConfig
    {
        public OptionFieldConfig(string code)
        {
            Code = code;
            Options = new List<SelectItem>();
        }

        public string Code { get; }
        public List<SelectItem> Options { get; set; }
    }
}