using DI.Security;

namespace DataTools
{
    public class MigUserProvider : IIdentityProvider
    {
        public IIdentity GetIdentity()
        {
            return new UserIdentity("", "SYSTEM", "");
        }
    }
}