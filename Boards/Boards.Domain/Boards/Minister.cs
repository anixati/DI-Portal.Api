using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Contacts;

namespace Boards.Domain.Boards
{
    [Table("Ministers")]
    public class Minister : ContactBaseEntity
    {
        public ICollection<MinisterTerm> Terms { get; set; }
        [MaxLength(255)] public string MigratedId { get; set; }
    }
}