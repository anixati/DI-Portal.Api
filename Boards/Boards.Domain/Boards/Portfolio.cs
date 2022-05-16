using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace Boards.Domain.Boards
{
    [Table("Portfolios")]
    public class Portfolio : NamedBaseEntity
    {
        public ICollection<Board> Boards { get; set; }

        public ICollection<MinisterTerm> Terms { get; set; }
    }

    public class PortfolioViewModel : NamedViewModel
    {
    }
}