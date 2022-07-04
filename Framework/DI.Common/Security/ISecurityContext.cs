using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Security.Core;

namespace DI.Security
{
    public interface ISecurityContext
    {
        string GetUserId();
        Task<List<long>> GetTeamIds();
        bool IsInRole(ApplicationRoles role);
        bool IsInRole(int role);
        bool HasRoles();
        int[] Roles();
        bool IsSysAdmin();
    }
}