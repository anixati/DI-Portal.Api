using DI.Core;
using DI.Domain.Core;

namespace DI.Response
{
    public class EntityResponse<T>:DomainResponse where T : class, IEntity
    {
        public EntityResponse(ResponseCode code, string message) : this(code, message, null)
        {
        }

        public EntityResponse(ResponseCode code, string message, T entity): base(code, message)
        {
            Entity = entity;
        }

        public T Entity { get; }
    }
}