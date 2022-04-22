using System.Net;

namespace DI.Exceptions
{
    public class InvalidRequestException : ServiceBaseException
    {
        public InvalidRequestException(string message, int errorCode = (int) HttpStatusCode.BadRequest) : base(message)
        {
            ErrorCode = errorCode;
        }

        public int ErrorCode { get; }
    }
}