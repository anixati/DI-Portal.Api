using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("TeamRoles", Schema = "ACL")]
    public class AppTeamRoles : IntersectBaseEntity
    {
        [Required]
        public long AppTeamId { get; set; }
        public AppTeam AppTeam { get; set; }
        [Required]
        public long AppRoleId { get; set; }
        public AppRole AppRole { get; set; }

    }
}