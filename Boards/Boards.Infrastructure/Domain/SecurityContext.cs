using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain;
using DI.Domain.Users;
using DI.Extensions;
using DI.Security;
using DI.Security.Core;

namespace Boards.Infrastructure.Domain
{
    public class SecurityContext : ISecurityContext
    {
        private readonly IBoardsContext _context;
        private readonly Lazy<IIdentity> _user;
        private List<long> _teams = null;
        public SecurityContext(IBoardsContext context, IIdentityProvider identityProvider)
        {
            _context = context;
            _user= new Lazy<IIdentity>(identityProvider.GetIdentity);
        }

        public IIdentity User => _user.Value;
        public string GetUserId()
        {
            return User.UserId;
        }

        public async Task<List<long>> GetTeamIds()
        {
            return _teams ??= await TeamIds();
        }

        private async Task<List<long>> TeamIds()
        {
            if (!User.AppUserId.HasValue) return new List<long>();
            var repo = _context.Repo<TeamUser>();
            var lst = await repo.GetListAsync(x => x.AppUserId == User.AppUserId.Value);
            var teamUsers = lst.ToList();
            return teamUsers.Any() ? teamUsers.Select(x => x.AppTeamId).ToList() : new List<long>();
        }

        public bool IsInRole(ApplicationRoles role)
        {
            return User.IsInRole(role);
        }

        public bool IsInRole(int role)
        {
            return User.IsInRole(role);
        }

        public bool HasRoles()
        {
            return User.HasRoles();
        }

        public int[] Roles()
        {
            return User.Roles();
        }

        public bool IsSysAdmin()
        {
            return User.IsSysAdmin();
        }
    }
}