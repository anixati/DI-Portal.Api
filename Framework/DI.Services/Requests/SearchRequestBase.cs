using DI.Core;

namespace DI.Services.Requests
{
    public abstract class SearchRequestBase : ISearchRequest
    {
        public int? Index { get; set; }
        public int? Size { get; set; }

        public PageCookie GetPageCookie()
        {
            return new PageCookie(Index ?? 1, Size ?? 20);
        }
    }
}