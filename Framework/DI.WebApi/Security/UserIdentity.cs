using DI.Security;
using DI.Security.Core;
using System;
using System.Linq;

namespace DI.WebApi.Security
{

    public class UserIdentity : IIdentity
    {
        private ApplicationRoles? _roles;
        private int[] _roleList = null;
        public UserIdentity(string userId, string name,string roles)
        {
            UserId = userId;
            Name = name;
            SetupRoles(roles);
        }

        private void SetupRoles(string roles)
        {
            if (string.IsNullOrEmpty(roles)) return;
            var rids = roles.Split('|', StringSplitOptions.RemoveEmptyEntries);
            _roleList = rids.Select((s, i) => int.TryParse(s, out i) ? i : 0).ToArray();
            var rs = _roleList.Aggregate((x, y) => x | y);
            _roles = (ApplicationRoles) rs;
        }
        public bool IsInRole(ApplicationRoles role)
        {
            return _roles.HasValue && _roles.Value.HasFlag(role);
        }
        public bool IsInRole(int role)
        {
            return _roleList != null && _roleList.Any(x => x == role);
        }

        public bool HasRoles()
        {
            return _roleList != null;
        }

        public string GetKey()
        {
            return $"{Name}|{UserId}";
        }

        public string UserId { get; }
        public string Name { get; }
    }
}
