using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Boards.Domain.Roles;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;

namespace Boards.Domain.Boards
{
    public class Board : NamedBaseEntity
    {
        [Required]
        public long PortfolioId { get; set; }
        public Portfolio Portfolio { get; set; }

        [MaxLength(2000)]
        public string Summary { get; set; }

        [MaxLength(2000)]
        public string PendingAction { get; set; }

        [MaxLength(2000)]
        public string EstablishedByUnderText{ get; set; }


        [MaxLength(255)]
        public string AssistantSecretory { get; set; }
        
        [MaxLength(50)]
        public string AssistantSecretoryPhone { get; set; }

        [MaxLength(255)]
        public string NominationCommittee { get; set; }

        [MaxLength(255)]
        public string OwnerDivision { get; set; }

        [MaxLength(255)]
        public string OwnerPosition { get; set; }

        //[MaxLength(255)]
        //public string BoardStatus { get; set; }

        [MaxLength(255)]
        public string LegislationReference { get; set; }

        [MaxLength(255)]
        public string Constitution { get; set; }

        [MaxLength(255)]
        public string ResponsibleOfficer { get; set; }

        [MaxLength(255)]
        public string QuorumRequiredText { get; set; }

        public int? OptimumMembers { get; set; }
        public int? MaximumTerms { get; set; }
        public int? MaximumMembers { get; set; }
        public int? MinimumMembers { get; set; }
        public int QuorumRequired { get; set; }

        public bool ReportingApproved { get; set; }
        
        public bool ExcludeFromGenderBalance { get; set; }

        public OptionSet Status { get; set; }
        public OptionSet Division { get; set; }
        public OptionSet EstablishedByUnder { get; set; }
        public OptionSet StatusColor { get; set; }
        public int? ApprovedAppUserId { get; set; }
        public AppUser Approved { get; set; }
        public List<BoardRole> Roles { get; set; }
    }
}