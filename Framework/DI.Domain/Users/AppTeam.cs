using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace DI.Domain.Users
{
    [Table("Teams", Schema = "ACL")]
    public class AppTeam : NamedBaseEntity
    {
    }
}