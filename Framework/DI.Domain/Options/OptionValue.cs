using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

namespace DI.Domain.Options
{
    public class OptionValue : AuditViewModel
    {
        public int OptionId { get; set; }
        [Required, MaxLength(255)]
        public string Label { get; set; }
        [Required]
        public int Value { get; set; }
        public short Order { get; set; }
        [MaxLength(500)]
        public string Description { get; set; }
    }
}