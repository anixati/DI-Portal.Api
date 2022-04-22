using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Boards.Domain.Roles;
using DI.Domain.Contacts;

namespace Boards.Domain.Contacts
{
    [Table("Secretaries")]
    public class AssistantSecretary : ContactBaseEntity
    {
        public List<BoardRole> Roles { get; set; }
    }
}