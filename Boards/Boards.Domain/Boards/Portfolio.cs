using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace Boards.Domain.Boards
{
    [Table("Portfolios")]
    public class Portfolio : NamedBaseEntity
    {
        public ICollection<Board> Boards { get; set; }

        public ICollection<MinisterTerm> Terms { get; set; }

        [MaxLength(255)] public string MigratedId { get; set; }
    }

    public class PortfolioViewModel : NamedViewModel
    {
    }
}