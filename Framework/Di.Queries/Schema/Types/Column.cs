namespace Di.Qry.Schema.Types
{
    public class Column
    {
        public Column()
        {
        }

        public Column(string colStr, string alias = "",string header="")
        {
            ColName = !string.IsNullOrEmpty(alias) ? $"{colStr} AS {alias}" : $"{colStr}";
            Accessor = !string.IsNullOrEmpty(alias) ? $"{alias}" : $"{colStr}";
            Header = !string.IsNullOrEmpty(header) ? $"{header}" : $"{Accessor}";
        }
        public string ColName { get; }
        public string Accessor { get; }
        public string Header { get; }
    }
}