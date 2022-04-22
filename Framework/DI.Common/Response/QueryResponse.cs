using DI.Core;
using DI.Domain.Core;
using DI.Domain.Data;

namespace DI.Response
{
    public class QueryResponse<T> : DomainResponse where T : class, IEntity
    {
        public QueryResponse(ResponseCode changeCode, string message) : this(changeCode, message, null)
        {
        }

        public QueryResponse(ResponseCode changeCode, string message, IPagedList<T> pagedList) : base(changeCode, message)
        {
            List = pagedList;
        }

        public IPagedList<T> List { get; }
    }
}