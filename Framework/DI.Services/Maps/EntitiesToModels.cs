﻿using AutoMapper;
using DI.Domain.Activities;
using DI.Domain.App;
using DI.Domain.Contacts;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;

namespace DI.Services.Maps
{
    public class EntitiesToModels : Profile
    {
        public EntitiesToModels()
        {
            CreateMap<BaseEntity, BaseViewModel>()
                .ForMember(x => x.IsDisabled, o => o.MapFrom(s => s.Disabled))
                .ForMember(x => x.IsLocked, o => o.MapFrom(s => s.Locked))
                .IncludeAllDerived();
            CreateMap<AuditBaseEntity, AuditViewModel>()
                .ForMember(x => x.CreatedByStamp, o => o.MapFrom(s => $"{s.CreatedBy} on {s.CreatedOn}"))
                .ForMember(x => x.ModifiedByStamp, o => o.MapFrom(s => $"{s.ModifiedBy} on {s.ModifiedOn}"))
                .IncludeAllDerived();
            CreateMap<NamedBaseEntity, NamedViewModel>()
                .IncludeAllDerived();
            CreateMap<ContactBaseEntity, ContactViewModel>()
                .IncludeAllDerived();
            CreateMap<ActivityBase, ActivityModelBase>()
                .IncludeAllDerived();

            CreateMap<Activity, ActivityViewModel>();


            CreateMap<OptionKey, OptionModel>();
            CreateMap<OptionSet, OptionValue>()
                .ForMember(x => x.OptionId, o => o.MapFrom(s => s.Id));


            CreateMap<AppUser, UserViewModel>();
            CreateMap<AppRole, RoleViewModel>();
            CreateMap<AppTeam, TeamViewModel>();

            CreateMap<AuditHistory, AuditModel>()
                .ForMember(x => x.Data, o => o.MapFrom(s => s.Data.ToList()));
        }
    }
}