using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using DI.Domain.Core;
using DI.Domain.Enums;
using DI.Domain.Options;

namespace Boards.Domain.Roles
{
    public class BoardAppointment : AuditBaseEntity
    {
        [Required, MaxLength(255), Column(Order = 1)]
        public string Name { get; set; }
        
        [Required] public long BoardId { get; set; }
        public virtual Board Board { get; set; }

        [Required] public long BoardRoleId { get; set; }
        public BoardRole BoardRole { get; set; }

        [Required] public long AppointeeId { get; set; }
        public Appointee Appointee { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [MaxLength(255)] public string BriefNumber { get; set; }


        public bool? IsExOfficio { get; set; }
        public bool IsFullTime { get; set; }
        public bool ActingInRole { get; set; }
        public bool? ExclGenderReport { get; set; }

        [Required, Column(TypeName = "decimal(13, 2)")] public decimal? AnnumAmount { get; set; }

        [Required] public long RemunerationPeriodId { get; set; }
        public virtual OptionSet RemunerationPeriod { get; set; }

        public long? AppointmentSourceSourceId { get; set; }
        public virtual OptionSet AppointmentSource { get; set; }

        public long? SelectionProcessProcessId { get; set; }

        public OptionSet SelectionProcess { get; set; }
        public long? JudicialId { get; set; }
        public OptionSet Judicial { get; set; }

        public DateTime? AppointmentDate { get; set; }
        public DateTime? InitialStartDate { get; set; }

        public int? PrevTerms { get; set; }
        public override string GetName()
        {
            return Name;
        }
    }
}