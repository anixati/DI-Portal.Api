using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DI.Domain.Core
{
    public abstract class NamedBaseEntity : AuditBaseEntity, INamedEntity
    {
        [Required]
        [MaxLength(255)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [MaxLength(2000)] public string Description { get; set; }

        public override string GetKey()
        {
            return Name;
        }
    }
}