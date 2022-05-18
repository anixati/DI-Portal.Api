namespace Di.Qry.Core
{
    public class PageInfo
    {
        public PageInfo():this(1,20)
        {
        }
        public PageInfo(int page,int pageSize)
        {
            CurrentPage = page;
            PageSize = pageSize;
        }
        public int Total { get; set; }
        public int CurrentPage { get;  }
        public int PageSize { get;  }
    }
}