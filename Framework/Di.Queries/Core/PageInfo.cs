namespace Di.Qry.Core
{
    public class PageInfo
    {
        public PageInfo()
        {
            CurrentPage = 1;
            PageSize = 100;
        }

        public int Total { get; set; }
        public int CurrentPage { get; set; }
        public int PageSize { get; set; }
    }
}