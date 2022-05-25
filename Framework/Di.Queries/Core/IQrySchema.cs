namespace Di.Qry.Core
{
    public interface IQrySchema
    {
        string SchemaName { get; }
        string Title { get; }
        SchemaType SchemaType { get; }
        IQryState Create();
    }
}