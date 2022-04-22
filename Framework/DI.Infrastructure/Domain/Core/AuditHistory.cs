using System;
using System.ComponentModel.DataAnnotations;

namespace DI.Domain.Core
{
    public class AuditHistory : BaseEntity
    {
        [Required] public DateTime AuditDate { get; set; }

        [Required] [StringLength(50)] public string Action { get; set; }

        [Required] [StringLength(50)] public string TableName { get; set; }

        public AuditProps Data { get; set; }
    }
}