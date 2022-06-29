using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Options;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Features
{
    [Index(nameof(EntityName),nameof(EntityId), IsUnique = true)]
    public class Activity : AuditBaseEntity
    {
        [Required, MaxLength(100)]
        public string EntityName { get; set; }
        [Required]
        public long EntityId { get; set; }

        [Required,MaxLength(500)] public string Title { get; set; }
        [MaxLength(10000)] public string Notes { get; set; }

        [MaxLength(255)]
        public string ContentType { get; set; }
    }
}
