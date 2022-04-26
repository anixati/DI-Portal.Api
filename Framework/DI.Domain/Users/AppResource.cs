using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Resources", Schema = Constants.SecuritySchema)]
    public class AppResource : NamedBaseEntity
    {
        public ICollection<Permission> Permissions { get; set; }
    }
}