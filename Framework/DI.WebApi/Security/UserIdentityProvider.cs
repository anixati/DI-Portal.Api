using System.Linq;
using System.Security.Claims;
using DI.Security;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Security
{
    public class UserIdentityProvider : IIdentityProvider
    {
        private const string cacheKey = "User";
        private readonly IHttpContextAccessor _accessor;
        private ILogger _logger;

        public UserIdentityProvider(ILoggerFactory loggerFactory, IHttpContextAccessor accessor)
        {
            _accessor = accessor;
            _logger = loggerFactory.CreateLogger(GetType().Name);
        }

        public IIdentity GetIdentity()
        {
            if (_accessor.HttpContext == null) return null;
            if (_accessor.HttpContext.Items.ContainsKey(cacheKey))
                return _accessor.HttpContext.Items[cacheKey] as IIdentity;

            var user = _accessor.HttpContext?.User;
            user.ThrowIfNull("Unable to retrieve user principal");

            var uid = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Sid)?.Value;
            var name = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            var roles = user.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Role)?.Value;

            var id = new UserIdentity(uid, name, roles);


            return id;
        }
    }
}