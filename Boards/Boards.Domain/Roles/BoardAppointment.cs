﻿using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Threading.Tasks;
using Boards.Domain.Boards;
using Boards.Domain.Contacts;
using Boards.Domain.Shared;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Services;

namespace Boards.Domain.Roles
{
    public class BoardAppointment : AuditBaseEntity
    {
        [Required]
        [MaxLength(255)]
        [Column(Order = 1)]
        public string Name { get; set; }

        [Required] public long BoardId { get; set; }
        public virtual Board Board { get; set; }

        [Required] public long BoardRoleId { get; set; }
        public virtual BoardRole BoardRole { get; set; }

        public long? AppointeeId { get; set; }
        public virtual Appointee Appointee { get; set; }

        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public bool EndDateUnknown { get; set; }
        [MaxLength(255)] public string BriefNumber { get; set; }


        public bool? IsCurrent { get; set; }
        public bool? IsExOfficio { get; set; }
        public bool ActingInRole { get; set; }
        public bool? ExclGenderReport { get; set; }

        public FullTimeEnum IsFullTime { get; set; }

        [Column(TypeName = "decimal(13, 2)")] public decimal? AnnumAmount { get; set; }

        public long? RemunerationPeriodId { get; set; }
        public virtual OptionSet RemunerationPeriod { get; set; }

        public long? AppointmentSourceId { get; set; }
        public virtual OptionSet AppointmentSource { get; set; }

        public long? SelectionProcessId { get; set; }
        public virtual OptionSet SelectionProcess { get; set; }

        public long? JudicialId { get; set; }
        public virtual OptionSet Judicial { get; set; }

        public DateTime? AppointmentDate { get; set; }
        public DateTime? InitialStartDate { get; set; }

        public int? PrevTerms { get; set; }

        public bool? IsSemiDiscretionary { get; set; }
        public bool? Proposed { get; set; }

        public long? AppointerId { get; set; }
        public virtual OptionSet Appointer { get; set; }

        [MaxLength(255)] public string MigratedId { get; set; }

        public override string GetName()
        {
            return Name;
        }

        public override async Task<IEntity> OnCoreEvent(EntityEvent @event, IDataStore store)
        {
            if (AppointeeId.HasValue)
            {
                var ps = await store.Repo<Appointee>().GetById(AppointeeId.Value);
                if (ps == null) return null;
                Name = ps.FullName;
            }

            if (@event == EntityEvent.Update) UpdateProposed();
            return this;
        }


        public void UpdateProposed()
        {
            if (Proposed.HasValue && Proposed.Value == true)
            {
                StartDate = null;
                EndDate = null;
                AppointmentDate = null;
                InitialStartDate = null;
            }

            if (EndDateUnknown == true) EndDate = null;
        }
    }
}