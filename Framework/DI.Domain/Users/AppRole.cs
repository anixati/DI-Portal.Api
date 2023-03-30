using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Users
{
    [Table("Roles", Schema = Constants.SecuritySchema)]
    [Index(nameof(Code), IsUnique = true)]
    public class AppRole : NamedBaseEntity, ICheckSystemEntity
    {
        [Required]
        [MaxLength(30)]
        [Column(Order = 2)]
        public string Code { get; set; }

        public virtual ICollection<UserRole> UserRoles { get; set; }
        public virtual ICollection<TeamRole> TeamRoles { get; set; }
        public virtual ICollection<Permission> Permissions { get; set; }

        [Required] public bool IsSystem { get; set; }
    }

    public class RoleViewModel : NamedViewModel
    {
        public RoleViewModel(string code)
        {
            Code = code;
        }


        public string Code { get; }
    }
}