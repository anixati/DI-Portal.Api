using System;
using System.ComponentModel.DataAnnotations;
using Boards.Domain.Contacts;
using DI.Domain.Enums;
using DI.Domain.Options;

namespace Boards.Domain.Roles
{
    public class BoardRoleEvent : BoardRoleBase
    {
        [Required] public long BoardRoleId { get; set; }
        public BoardRole BoardRole { get; set; }

        [Required] public long AppointeeId { get; set; }
        public Appointee Appointee { get; set; }


        public DateTime? AppointmentDate { get; set; }
        public DateTime InitialStartDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }


        [MaxLength(255)] public string BriefNumber { get; set; }

        public YesNoNaEnum IsRemunerated { get; set; }

        public OptionSet TermsServed { get; set; }
        public OptionSet AppointmentStatus { get; set; }


        public bool? EndDateUnknown { get; set; }
        public bool? IsProposed { get; set; }
        public bool? IsCurrentAppointment { get; set; }
        public bool? IsActing { get; set; }
        public int? AppointedDays { get; set; }
        public int? AppointmentDays { get; set; }
    }
}