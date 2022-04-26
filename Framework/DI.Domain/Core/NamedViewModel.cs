using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DI.Domain.Core
{
    public abstract class NamedViewModel : AuditViewModel
    {
        [Required]
        [MaxLength(255)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [MaxLength(2000)] public string Description { get; set; }
    }
}