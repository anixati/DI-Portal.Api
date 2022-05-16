using System.Text;

namespace Di.Qry.Core
{
    public class PagedContext: IPagedContext
    {
        public PageInfo PageInfo { get; set; }
        public IQryContext CountQry { get; set; }
        public IQryContext DataQry { get; set; }
        public bool RequireFilter { get; set; }
        public bool EmptyFilter { get; set; }

        public override string ToString()
        {
            var sbr = new StringBuilder();
            sbr.AppendLine($"Paging Info :");
            sbr.AppendLine($"{PageInfo}");
            sbr.AppendLine($"Count Query :");
            sbr.AppendLine($"{CountQry}");
            sbr.AppendLine($"Data Query :");
            sbr.AppendLine($"{DataQry}");
            return sbr.ToString();
        }
    }
}