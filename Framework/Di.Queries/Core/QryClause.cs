namespace Di.Qry.Core
{
    public class QryClause
    {
        public QryClause(string opKey, object opValue)
        {
            Operator = opKey;
            Value = opValue;
        }

        public string Operator { get; }
        public object Value { get; }
    }
}