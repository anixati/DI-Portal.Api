using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Roles", Schema = "ACL")]
    public class AppRole : NamedBaseEntity
    {
        [Required, MaxLength(30), Column(Order = 2)]
        public string Code { get; set; }

    }
}