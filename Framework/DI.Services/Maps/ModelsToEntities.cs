﻿using AutoMapper;
using DI.Domain.Contacts;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;

namespace DI.Services.Maps
{
    public class ModelsToEntities : Profile
    {
        public ModelsToEntities()
        {
            CreateMap<BaseViewModel, BaseEntity>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .IncludeAllDerived();
            CreateMap<NamedViewModel, NamedBaseEntity>()
                .IncludeAllDerived();
            CreateMap<ContactViewModel, ContactBaseEntity>()
                .IncludeAllDerived();
            CreateMap<OptionModel, OptionKey>(MemberList.Source);
            CreateMap<OptionValue, OptionSet>();


            CreateMap<UserViewModel, AppUser>();
            CreateMap<RoleViewModel, AppRole>();
            CreateMap<TeamViewModel, AppTeam>();
        }
    }
}