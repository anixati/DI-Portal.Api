using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DI.Domain.Core
{
    public abstract class AuditBaseEntity : BaseEntity, IAuditEntity
    {
        [Required, Column(Order = 800)] public DateTime CreatedOn { get; set; }

        [Required, Column(Order = 801)] [StringLength(50)] public string CreatedBy { get; set; }

        [Required, Column(Order = 802)] public DateTime ModifiedOn { get; set; }

        [Required, Column(Order = 803)] [StringLength(50)] public string ModifiedBy { get; set; }
    }
}