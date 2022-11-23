using System;
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
        [MaxLength(500)] public virtual string NameId { get; set; }
        [MaxLength(500)] public virtual string Upn { get; set; }
        [MaxLength(500)] public virtual string DisplayName { get; set; }
        [MaxLength(500)] public string JobRole { get; set; }
        public virtual DateTime? AccessRequest { get; set; }
       public virtual DateTime? AccessGranted { get; set; }

        [Required]
        public bool IsSystem { get; set; }
        public virtual ICollection<TeamUser> UserTeams { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

        [MaxLength(255)] public string MigratedId { get; set; }
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