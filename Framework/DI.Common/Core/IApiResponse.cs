using System.Collections.Generic;

namespace DI.Core
{
    public interface ISearchRequest
    {
        PageCookie GetPageCookie();
    }

    public interface IApiResponse
    {
        ResponseCode Reason { get; }
        IReadOnlyCollection<string> Messages { get; }
        bool Failed { get; }
        object Result { get; }
    }
}