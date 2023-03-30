using Newtonsoft.Json;

namespace DI.Forms.Types
{
    public class SelectItem
    {
        public SelectItem(string value, string label)
        {
            Value = value;
            Label = label;
        }
        [JsonProperty("label")]
        public string Label { get; }

        [JsonProperty("value")]
        public string Value { get; }
    }

    
}