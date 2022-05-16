namespace Di.Qry.Core
{
    public interface IPagedContext
    {
        bool RequireFilter { get; set; }
        bool EmptyFilter { get; set; }
        PageInfo PageInfo { get; }
        IQryContext CountQry { get; }
        IQryContext DataQry { get; }
    }
}