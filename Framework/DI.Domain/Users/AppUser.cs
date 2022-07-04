using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Contacts;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Users
{
    [Table("Users", Schema = Constants.SecuritySchema)]
    [Index(nameof(UserId), IsUnique = true)]
    public class AppUser : ContactBaseEntity, ICheckSystemEntity
    {
        [Required] [MaxLength(255)] public virtual string UserId { get; set; }

        [Required][MaxLength(int.MaxValue)] public virtual string PasswordHash { get; set; }
        [Required][MaxLength(int.MaxValue)] public virtual string SecurityStamp { get; set; }
        public virtual bool ChangePassword { get; set; }
        public virtual  bool EmailConfirmed { get; set; }
        public virtual bool LockedOut { get; set; }
        public virtual int AccessFailCount { get; set; }


        [Required]
        public bool IsSystem { get; set; }
        public virtual ICollection<TeamUser> UserTeams { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }

    public class UserViewModel : ContactViewModel
    {
        public UserViewModel(string userId)
        {
            UserId = userId;
        }

        public string UserId { get; }
    }
}