namespace Di.Qry.Contracts
{
    public interface IQrySchema
    {
        string SchemaName { get; }
        bool RefData { get; }
        IQryState Create();
    }
}