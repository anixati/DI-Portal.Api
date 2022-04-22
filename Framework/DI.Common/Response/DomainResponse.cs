using DI.Core;

namespace DI.Response
{
    public class DomainResponse 
    {
        public DomainResponse(ResponseCode code, string message)
        {
            ChangeCode = code;
            Message = message;
        }

        public ResponseCode ChangeCode { get; }
        public string Message { get; }
    }
}