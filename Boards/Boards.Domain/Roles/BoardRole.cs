using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Autofac.Features.ResolveAnything;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using Boards.Domain.Shared;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Services;

namespace Boards.Domain.Roles
{
    public class BoardRole : AuditBaseEntity
    {

        [Required, MaxLength(255), Column(Order = 1)]
        public string Name { get; set; }

        [Required] public long BoardId { get; set; }
        public virtual Board Board { get; set; }
        
        public long? IncumbentId { get; set; }
        public virtual Appointee Incumbent { get; set; }
        

        [Required] public long PositionId { get; set; }
        public OptionSet Position { get; set; }
        
        [Required] public long RoleAppointerId { get; set; }
        public OptionSet RoleAppointer { get; set; }

        public bool IsFullTime { get; set; }
        public bool? IsExecutive { get; set; }
        public bool? IsExOfficio { get; set; }
        public bool? IsApsEmployee { get; set; }
        public bool? IsExNominated { get; set; }
        public int? Term { get; set; }
        
        [Required] public YesNoOptionEnum PositionRemunerated { get; set; }
        [Required,Column(TypeName = "decimal(13, 2)")] public decimal? PaAmount { get; set; }
        [Required] public long RemunerationMethodId { get; set; }
        public virtual OptionSet RemunerationMethod { get; set; }


        [Required, MaxLength(255), Column(Order = 1)]
        public string RemunerationTribunal { get; set; }
        public DateTime? VacantFromDate { get; set; }

        public bool? ExcludeFromOrder15 { get; set; }
        public bool? ExcludeGenderReport { get; set; }
        public bool IsSignAppointment { get; set; }


        [MaxLength(2000)] public string NextSteps { get; set; }


        //-------- Ministerial-----------

        [MaxLength(2000)] public string InstrumentLink { get; set; }
        [Required] public string PDMSNumber { get; set; }
        [Required] public long MinSubLocationId { get; set; }
        public virtual OptionSet MinSubLocation { get; set; }
        public DateTime? MinisterOfficeDate { get; set; }
        public DateTime? MinisterActionDate { get; set; }

       

        public DateStateEnum LetterToPmDateType { get; set; }
        public DateTime? LetterToPmDate { get; set; }

        public DateStateEnum ExCoDateType { get; set; }
        public DateTime? ExCoDate { get; set; }
        
        public DateStateEnum NotifyLetterDateType { get; set; }
        public DateTime? NotifyLetterDate { get; set; }
        
        public DateStateEnum CabinetDateType { get; set; }
        public DateTime? CabinetDate { get; set; }

        [MaxLength(2000)] public string InternalNotes { get; set; }

        
        [MaxLength(2000)] public string ProcessStatus { get; set; }
        public int? LeadTimeToAppoint { get; set; }
        
        public DateStateEnum MinSubDateType { get; set; }
        public DateTime? MinSubDate { get; set; }


        public override string GetName()
        {
            return Name;
        }
         
        public override async Task<IEntity> OnCoreEvent(EntityEvent @event, IDataStore store)
        {
            var ps = await store.Repo<OptionSet>().GetById(this.PositionId);
            if (ps == null) return null;
            this.Name = ps.Label;
            return this;

        }
        [MaxLength(255)] public string MigratedId { get; set; }
    }
}