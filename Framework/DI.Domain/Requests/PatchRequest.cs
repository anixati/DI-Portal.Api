using Microsoft.AspNetCore.JsonPatch;

namespace DI.Domain.Requests
{
    public class PatchRequest
    {
        protected PatchRequest(long id, JsonPatchDocument request)
        {
            Id = id;
            Request = request;
        }

        public long Id { get; }
        public JsonPatchDocument Request { get; }
    }
}