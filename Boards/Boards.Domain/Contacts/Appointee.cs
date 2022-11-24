using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boards.Domain.Roles;
using DI.Domain.Contacts;
using DI.Domain.Enums;
using DI.Domain.Options;

namespace Boards.Domain.Contacts
{
    public class Appointee : ContactBaseEntity
    {
        [MaxLength(2000)] public string Biography { get; set; }

        [MaxLength(255)] public string PostNominals { get; set; }

        [MaxLength(255)] public string ResumeLink { get; set; }

        [MaxLength(255)] public string LinkedInProfile { get; set; }

        public RegionalEnum IsRegional { get; set; }
        public YesNoExEnum IsAboriginal { get; set; }
        public YesNoExEnum IsDisabled { get; set; }
        public YesNoExEnum IsCAlDBackground { get; set; }
        public YesNoEnum ExecutiveSearch { get; set; }

        public long? CapabilitiesId { get; set; }
        public virtual OptionSet Capabilities { get; set; }

        public long? ExperienceId { get; set; }
        public virtual OptionSet Experience { get; set; }
        public virtual ICollection<BoardRole> Roles { get; set; }
        public virtual ICollection<AppointeeSkill> AppointeeSkills { get; set; }
        [MaxLength(255)] public string MigratedId { get; set; }
    }
}