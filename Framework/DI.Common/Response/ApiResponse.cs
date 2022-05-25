using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using DI.Core;
using Newtonsoft.Json;

namespace DI.Response
{
    public class ApiResponse : IApiResponse
    {
        public ApiResponse(ResponseCode status, object result, IList<string> messages = null)
        {
            Reason = status;
            Messages = messages != null ? new ReadOnlyCollection<string>(messages) : null;
            Failed = (int) status > 1;
            Result = result;
        }

        public ResponseCode Reason { get; }
        public IReadOnlyCollection<string> Messages { get; }
        public bool Failed { get; }
        public object Result { get; }

        public override string ToString()
        {
            return JsonConvert.SerializeObject(this);
        }

        public static IApiResponse Success(object result)
        {
            return new ApiResponse(ResponseCode.Default, result);
        }


        public static IApiResponse Error(Exception exception, ResponseCode status = ResponseCode.ServerError)
        {
            return Error(exception.ToExceptionMessages(), status);
        }


        public static IApiResponse Error(string message, ResponseCode status = ResponseCode.ServerError)
        {
            return Error(new List<string> {message}, status);
        }

        public static IApiResponse Error(List<string> messages, ResponseCode status = ResponseCode.ServerError)
        {
            return new ApiResponse(status, null, messages);
        }
    }
}