namespace DI.Forms.Types
{
    public class FormAction {

        public string Label { get;  }

        public FormAction(string schema, string label, string desc ="")
        {
            Label = label;
            Schema = schema;
            Description = desc;
            Visible = true;
        }
        public string Schema { get; }
        public string Description { get; set; }
        public bool Visible { get; set; }
    }
}