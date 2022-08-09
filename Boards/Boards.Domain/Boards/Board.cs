using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Boards.Domain.Contacts;
using Boards.Domain.Roles;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;

namespace Boards.Domain.Boards
{
    public class Board : NamedBaseEntity
    {
        [Required] public long PortfolioId { get; set; }

        public virtual Portfolio Portfolio { get; set; }

        [Required] public long AppTeamId { get; set; }

        public virtual AppTeam AppTeam { get; set; }


        [MaxLength(2000)] public string Summary { get; set; }

        [MaxLength(2000)] public string PendingAction { get; set; }

        [MaxLength(2000)] public string EstablishedByUnderText { get; set; }

       

        [MaxLength(255)] public string NominationCommittee { get; set; }

        public long OwnerDivisionId { get; set; }
        public OptionSet OwnerDivision { get; set; }
        public long OwnerPositionId { get; set; }
        public OptionSet OwnerPosition { get; set; }

        [MaxLength(255)] public string Acronym { get; set; }

        //[MaxLength(255)]
        //public string BoardStatus { get; set; }

        [MaxLength(255)] public string LegislationReference { get; set; }

        [MaxLength(255)] public string Constitution { get; set; }

        [MaxLength(255)] public string QuorumRequiredText { get; set; }

        public int? OptimumMembers { get; set; }
        public int? MaximumTerms { get; set; }
        public int? MaximumMembers { get; set; }
        public int? MinimumMembers { get; set; }
        public int QuorumRequired { get; set; }

        public bool ReportingApproved { get; set; }

        public bool ExcludeFromGenderBalance { get; set; }

        public long? BoardStatusId { get; set; }
        public OptionSet BoardStatus { get; set; }
        
        //public long? DivisionId { get; set; }
        //public OptionSet Division { get; set; }

        public long? EstablishedByUnderId { get; set; }
        public OptionSet EstablishedByUnder { get; set; }
        

        public long? ApprovedUserId { get; set; }
        public virtual AppUser ApprovedUser { get; set; }

        public long? ResponsibleUserId { get; set; }
        public virtual AppUser ResponsibleUser { get; set; }

        public long? AsstSecretaryId { get; set; }
        public virtual AssistantSecretary AsstSecretary { get; set; }
        [MaxLength(50)] public string AsstSecretaryPhone { get; set; }

        public List<BoardRole> Roles { get; set; }

        public int? MaxServicePeriod { get; set; }

        [MaxLength(255)] public string MigratedId { get; set; }
    }
}