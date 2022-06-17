using System;
using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;

namespace Boards.Domain.Boards
{
    [Table("MinisterTerms")]
    public class MinisterTerm : IntersectBaseEntity
    {
        public long MinisterId { get; set; }
        public Minister Minister { get; set; }
        public long PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}