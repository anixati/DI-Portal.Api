using System.Collections.Generic;
using DI.Domain.Contacts;

namespace Boards.Domain.Boards
{
    public class Minister : ContactBaseEntity
    {
        public ICollection<MinisterTerm> Terms { get; set; }
    }
}