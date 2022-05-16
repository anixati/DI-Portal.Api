using Di.Qry.Core;

namespace Di.Qry.Schema
{
    public abstract class RefDataSchema : QrySchema
    {
        public override SchemaType SchemaType => SchemaType.RefDataQuery;
    }
}