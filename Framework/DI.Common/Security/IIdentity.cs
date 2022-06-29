using System;
using System.Linq;
using DI.Core;
using DI.Security.Core;

namespace DI.Security
{
    public interface IIdentity : IComponent
    {
        long? AppUserId { get; }
        string UserId { get; }
        string Name { get; }
        bool IsInRole(ApplicationRoles role);
        bool IsInRole(int role);
        bool HasRoles();
        int[] Roles();
    }


    public class UserIdentity : IIdentity
    {
        private ApplicationRoles? _roles;
        private int[] _roleList = null;
        public UserIdentity(string userId, string name, string roles)
        {
            UserId = userId;
            Name = name;
            SetupRoles(roles);
            if (long.TryParse(userId, out var id))
                AppUserId = id;
            else
                AppUserId = null;
        }

        private void SetupRoles(string roles)
        {
            if (string.IsNullOrEmpty(roles)) return;
            var rids = roles.Split('|', StringSplitOptions.RemoveEmptyEntries);
            _roleList = rids.Select((s, i) => int.TryParse(s, out i) ? i : 0).ToArray();
            var rs = _roleList.Aggregate((x, y) => x | y);
            _roles = (ApplicationRoles)rs;
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

        public int[] Roles()
        {
            return _roleList;
        }

        public string GetKey()
        {
            return $"{Name}|{UserId}";
        }

        public long? AppUserId { get; }
        public string UserId { get; }
        public string Name { get; }
    }
}