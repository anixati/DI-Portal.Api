using System.Collections.Generic;

namespace DI.Core
{
    public interface IApiResponse
    {
        ResponseCode Reason { get; }
        IReadOnlyCollection<string> Messages { get; }
        bool Failed { get; }
        object Result { get; }
    }
}