using DI.Core;

namespace DI.Response
{
    public class ActionResponse
    {
        public ActionResponse(ResponseCode changeCode, string message, long? id = null) : this(changeCode, message)
        {
            Id = id;
        }

        public ActionResponse(ResponseCode code, string message)
        {
            ChangeCode = code;
            Message = message;
        }

        public long? Id { get; }
        public ResponseCode ChangeCode { get; }
        public string Message { get; }
    }
}