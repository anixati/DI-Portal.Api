using DI.Core;
using DI.Domain.Core;

namespace DI.Response
{
    public class ViewResponse<T> : DomainResponse where T : class, IViewModel
    {
        public ViewResponse(ResponseCode code, string message) : this(code, message, null)
        {
        }

        public ViewResponse(ResponseCode code, string message, T item) : base(code, message)
        {
            Item = item;
        }

        public T Item { get; }
    }
}