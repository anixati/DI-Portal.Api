namespace Di.Qry.Core
{
    public interface IQrySchema
    {
        string SchemaName { get; }
        SchemaType SchemaType { get; }
        IQryState Create();
    }
}