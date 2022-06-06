using Microsoft.AspNetCore.JsonPatch;

namespace DI.Domain.Requests
{
    public class PatchRequest
    {
        protected PatchRequest(long id, JsonPatchDocument data)
        {
            Id = id;
            Data = data;
        }

        public long Id { get; }
        public JsonPatchDocument Data { get; }
    }
}