using DI.Core;
using DI.Security.Core;

namespace DI.Security
{
    public interface IIdentity : IComponent
    {
        string UserId { get; }
        string Name { get; }
        bool IsInRole(ApplicationRoles role);
        bool IsInRole(int role);
        bool HasRoles();
    }
}