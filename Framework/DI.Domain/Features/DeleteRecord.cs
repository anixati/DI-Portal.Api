using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Features
{
    [Index(nameof(EntityName), nameof(EntityId), IsUnique = true)]
    public class DeleteRecord : AuditBaseEntity
    {
        [Required, MaxLength(100)]
        public string EntityName { get; set; }
        [Required]
        public long EntityId { get; set; }
        [MaxLength(500)] public string Notes { get; set; }
    }
}