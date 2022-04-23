using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Contacts;

namespace DI.Domain.Users
{
    [Table("Users", Schema = "ACL")]
    public class AppUser : ContactBaseEntity
    {
        [Required,MaxLength(255)]
        public string UserId { get; set; }


        public ICollection<TeamUser> UserTeams { get; set; }
    }
}