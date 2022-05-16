namespace Di.Qry.Schema.Types
{
    public class SubEntity : Entity
    {
        private readonly string _fromKey;

        public SubEntity(string name, string alias, string fromKey,
            string schemaName = "dbo")
            : base(name, alias, schemaName)
        {
            _fromKey = fromKey;
        }

        public string FromKey => $"{_fromKey}";
    }
}