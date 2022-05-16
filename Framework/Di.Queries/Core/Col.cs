namespace Di.Qry.Core
{
    public class Col
    {
        public Col()
        {
        }

        public Col(string colStr, string alias = "")
        {
            Column = !string.IsNullOrEmpty(alias) ? $"{colStr} AS {alias}" : $"{colStr}";
        }

        public string Column { get; }
    }
}