namespace DI.Security
{
    public interface IIdentityProvider
    {
        IIdentity GetIdentity();
    }
}