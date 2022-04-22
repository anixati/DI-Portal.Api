using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using Boards.Domain.Shared;
using DI.Domain.Options;

namespace Boards.Domain.Roles
{
    public class BoardRole : BoardRoleBase
    {

        [Required, MaxLength(255), Column(Order = 1)]
        public string Name { get; set; }

        [MaxLength(2000)]
        public string Description { get; set; }

        [Required]
        public long BoardId { get; set; }
        public Board Board { get; set; }


        [Required]
        public long IncumbentId { get; set; }
        public Appointee Incumbent { get; set; }


        [Required]
        public long? AssistantSecretaryId { get; set; }
        public AssistantSecretary AssistantSecretary { get; set; }

        
        public DateTime? MinisterLetterDate { get; set; }
        public DateTime? MinisterSubDate { get; set; }
        public DateTime? MinisterActionDate { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public DateTime? CabinetDate { get; set; }
        public DateTime? ExCoDate { get; set; }
        public DateTime? VacantFromDate { get; set; }
        public DateTime? NotifyLetterDate { get; set; }

        [MaxLength(2000)]
        public string ProcessStatus { get; set; }
        [MaxLength(2000)]
        public string Responsibilities { get; set; }
        [MaxLength(2000)]
        public string Requirements { get; set; }
        [MaxLength(2000)]
        public string NextSteps { get; set; }
       
        public DateStateEnum MinLetterDateType { get; set; }
        public DateStateEnum NotLetterDateType { get; set; }
        public DateStateEnum ExCoDateType { get; set; }
        public DateStateEnum CabinetDateType { get; set; }
        public OptionSet PositionType { get; set; }

        public bool? IsApsEmployee { get; set; }
        public bool? IsSignificant { get; set; }
        public bool? IsExecutive { get; set; }
        public OptionSet ReasonForGenderExclude { get; set; }
        public OptionSet EstablishedByUnder { get; set; }
        public OptionSet MinSubLocation { get; set; }

    }
}