using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

namespace Boards.Domain.Contacts
{
    public class AppointeeSkill : IntersectBaseEntity
    {
        [Required] public long AppointeeId { get; set; }

        public Appointee Appointee { get; set; }


        [Required] public long SkillId { get; set; }

        public Skill Skill { get; set; }
    }
}