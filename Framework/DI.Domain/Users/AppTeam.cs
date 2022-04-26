using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Teams", Schema = Constants.SecuritySchema)]
    public class AppTeam : NamedBaseEntity
    {
        public ICollection<TeamUser> TeamUsers { get; set; }
        public ICollection<TeamRole> TeamRoles { get; set; }
    }

    public class TeamViewModel : NamedViewModel
    {
    }
}