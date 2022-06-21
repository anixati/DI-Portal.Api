namespace DI.Forms.Types
{
    public class SelectItem
    {
        public SelectItem(string value, string label)
        {
            Value = value;
            Label = label;
        }

        public string Label { get; }
        public string Value { get;  }
    }
}