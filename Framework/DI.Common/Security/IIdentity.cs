using DI.Core;

namespace DI.Security
{
    public interface IIdentity : IComponent
    {
        string UserId { get; }
        string Name { get; }
    }
}