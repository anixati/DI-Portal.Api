using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Options;

namespace DI.Services.Maps
{
    public class ModelsToEntities : Profile
    {
        public ModelsToEntities()
        {
            CreateMap<BaseViewModel, BaseEntity>()
                .ForMember(x => x.Id, o => o.MapFrom(s => s.Id))
                .IncludeAllDerived();

            CreateMap<OptionModel, OptionKey>(MemberList.Source);
            CreateMap<OptionValue, OptionSet>();
        }
    }
}