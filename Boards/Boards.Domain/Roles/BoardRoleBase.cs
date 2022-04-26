using System.ComponentModel.DataAnnotations.Schema;
using DI.Domain.Core;
using DI.Domain.Options;

namespace Boards.Domain.Roles
{
    public abstract class BoardRoleBase : AuditBaseEntity
    {
        public OptionSet AppointmentSource { get; set; }
        public OptionSet RemunerationPeriod { get; set; }
        public OptionSet ApproverType { get; set; }
        public OptionSet RemunerationMethod { get; set; }
        public OptionSet AppointmentState { get; set; }
        public OptionSet Source { get; set; }
        public OptionSet SelectionProcess { get; set; }

        public OptionSet Position { get; set; }
        public OptionSet PositionRemunerated { get; set; }
        public OptionSet Judicial { get; set; }

        public bool? ExcludeFromOrder15 { get; set; }
        public bool? IsSemiDisc { get; set; }
        public bool? ExOfficio { get; set; }
        public bool? IsFullTime { get; set; }
        public bool? ExcludeGenderReport { get; set; }
        public int? TermLimit { get; set; }
        public int? LeadTimeMonths { get; set; }
        public int? TermYears { get; set; }

        public int? MaxService { get; set; }

        #region Remuneration

        [Column(TypeName = "decimal(13, 2)")] public decimal? PdAmount { get; set; }

        [Column(TypeName = "decimal(13, 2)")] public decimal? PdRemuneration { get; set; }

        public decimal? ExchangeRate { get; set; }

        [Column(TypeName = "decimal(13, 2)")] public decimal? PaAmount { get; set; }

        [Column(TypeName = "decimal(13, 2)")] public decimal? PaRemuneration { get; set; }

        #endregion
    }
}