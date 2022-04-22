using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace Boards.Domain.Boards
{
    [Table("Portfolios")]
    public class Portfolio : NamedBaseEntity
    {

        public List<Board> Boards { get; set; }
    }
}