using System;
using System.ComponentModel.DataAnnotations;
using DI.Domain.Core;

namespace DI.Domain.AutoNo
{
    public class AutoNumberEntity : BaseEntity
    {
        public Guid? TenantId { get; set; }

        [Required] public string Name { get; set; }

        [Required] public int Number { get; set; }
    }
}