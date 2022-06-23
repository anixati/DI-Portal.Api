using DI.Security;
using DI.Security.Core;

namespace DI.Extensions
{
    public static class SecurityExtensions
    {

        public static bool IsAdmin(this IIdentity user)
        {
           return user.IsInRole(ApplicationRoles.Admin) || user.IsInRole(ApplicationRoles.SysAdmin);
        }
        public static bool IsSysAdmin(this IIdentity user)
        {
            return user.IsInRole(ApplicationRoles.SysAdmin);
        }
    }
}