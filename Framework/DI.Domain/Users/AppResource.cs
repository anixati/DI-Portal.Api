using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Resources", Schema = "ACL")]
    public class AppResource : NamedBaseEntity
    {

        public ICollection<Permission> Permissions { get; set; }
    }
}