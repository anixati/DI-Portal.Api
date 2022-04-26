using DI.Core;

namespace DI.Response
{
    public class DomainResponse
    {
        public DomainResponse(ResponseCode code, string message)
        {
            ResponseCode = code;
            Message = message;
        }

        public ResponseCode ResponseCode { get; }
        public string Message { get; }
    }
}