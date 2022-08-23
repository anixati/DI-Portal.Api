using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;
using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Activities
{
    //[Index(nameof(EntityName), nameof(EntityId), IsUnique = true)]
    public class ActivityBase : AuditBaseEntity
    {
        [Required, MaxLength(100)] public string EntityName { get; set; }
        [Required] public long EntityId { get; set; }

    }

    public class ActivityModelBase : AuditViewModel
    {
        [Required, MaxLength(100)]
        public string EntityName { get; set; }
        [Required]
        public long EntityId { get; set; }
    }
}