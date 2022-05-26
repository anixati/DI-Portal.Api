namespace Di.Qry.Schema.Types
{
    public class SubTable : Table
    {
        private readonly string _fromKey;

        public SubTable(string name, string alias, string fromKey,
            string schemaName = "dbo")
            : base(name, alias, schemaName)
        {
            _fromKey = fromKey;
        }

        public string FromKey => $"{_fromKey}";
    }
}