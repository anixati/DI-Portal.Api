using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Options
{
    [Index(nameof(Code), IsUnique = true)]
    public class OptionKey : NamedBaseEntity
    {
        [Required]
        [MaxLength(100)]
        [Column(Order = 2)]
        public string Code { get; set; }

        public List<OptionSet> Values { get; set; }
    }
}