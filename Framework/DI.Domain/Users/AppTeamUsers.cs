﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("TeamUsers", Schema = "ACL")]
    public class AppTeamUsers : IntersectBaseEntity
    {
        [Required]
        public long AppTeamId { get; set; }
        public AppTeam AppTeam { get; set; }
        [Required]
        public long AppUserId { get; set; }
        public AppUser AppUser { get; set; }
    }
}