using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;
using DI.Domain.Options;

namespace Boards.Domain.Contacts
{
    public class Skill : NamedBaseEntity
    {
        [Required]public long SkillTypeId { get; set; }
        public OptionSet SkillType { get; set; }
        public virtual ICollection<AppointeeSkill> AppointeeSkills { get; set; }
    }
}