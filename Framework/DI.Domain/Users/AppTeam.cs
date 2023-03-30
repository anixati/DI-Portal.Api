using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Teams", Schema = Constants.SecuritySchema)]
    public class AppTeam : NamedBaseEntity, ICheckSystemEntity
    {
        public virtual ICollection<TeamUser> TeamUsers { get; set; }
        public virtual ICollection<TeamRole> TeamRoles { get; set; }

        [Required] public bool IsSystem { get; set; }
    }

    public class TeamViewModel : NamedViewModel
    {
    }
}