using DI.Domain.Core;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DI.Domain.App
{
    public class AuditHistory : BaseEntity
    {
        [Required] public DateTime AuditDate { get; set; }

        [Required][StringLength(50)] public string Action { get; set; }

        [Required][StringLength(50)] public string TableName { get; set; }

        public AuditProps Data { get; set; } = new AuditProps();
    }




    public class AuditModel : BaseViewModel
    {
        public DateTime AuditDate { get; set; }

        public string Action { get; set; }

        public string TableName { get; set; }

        public List<PropModel> Data { get; set; } = new List<PropModel>();
    }
}