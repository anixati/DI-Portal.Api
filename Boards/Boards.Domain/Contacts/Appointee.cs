using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boards.Domain.Roles;
using DI.Domain.Contacts;
using DI.Domain.Options;

namespace Boards.Domain.Contacts
{
    public class Appointee : ContactBaseEntity
    {
        [MaxLength(2000)] public string Biography { get; set; }

        [MaxLength(255)] public string PostNominals { get; set; }

        [MaxLength(255)] public string ResumeLink { get; set; }

        [MaxLength(255)] public string LinkedInProfile { get; set; }

        public bool? IsRegional { get; set; }
        public bool? IsAboriginal { get; set; }
        public bool? IsDisabled { get; set; }
        public bool? ExecutiveSearch { get; set; }

        public long? CapabilitiesId { get; set; }
        public virtual OptionSet Capabilities { get; set; }

        public long? ExperienceId { get; set; }
        public virtual OptionSet Experience { get; set; }
        public virtual ICollection<BoardRole> Roles { get; set; }
        public virtual ICollection<AppointeeSkill> AppointeeSkills { get; set; }
        [MaxLength(255)] public string MigratedId { get; set; }
    }
}