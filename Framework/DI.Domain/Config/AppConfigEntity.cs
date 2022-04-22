using System;
using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

namespace DI.Domain.Config
{
    public class AppConfigEntity : AuditBaseEntity
    {
        public Guid? TenantId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public string Value { get; set; }
    }
}