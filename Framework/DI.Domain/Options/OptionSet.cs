using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Options
{
    public class OptionSet : AuditBaseEntity
    {
        [Required] public long OptionKeyId { get; set; }

        public OptionKey OptionKey { get; set; }

        [Required]
        [MaxLength(255)]
        [Column(Order = 1)]
        public string Label { get; set; }

        [Required] [Column(Order = 2)] public int Value { get; set; }

        [Column(Order = 3)] public int Order { get; set; }

        [MaxLength(500)] public string Description { get; set; }
    }
}