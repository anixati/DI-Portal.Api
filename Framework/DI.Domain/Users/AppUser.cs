using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Contacts;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Users
{
    [Table("Users", Schema = Constants.SecuritySchema)]
    [Index(nameof(UserId), IsUnique = true)]
    public class AppUser : ContactBaseEntity
    {
        [Required] [MaxLength(255)] public string UserId { get; set; }
        public ICollection<TeamUser> UserTeams { get; set; }
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