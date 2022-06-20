using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("UserRoles", Schema = Constants.SecuritySchema)]
    public class UserRole : IntersectBaseEntity
    {
        [Required] public long AppUserId { get; set; }

        public AppUser AppUser { get; set; }


        [Required] public long AppRoleId { get; set; }

        public AppRole AppRole { get; set; }
    }
}