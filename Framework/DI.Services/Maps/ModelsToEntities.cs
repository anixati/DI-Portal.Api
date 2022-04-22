using AutoMapper;
using DI.Domain.Core;
using DI.Domain.Options;

namespace DI.Services.Maps
{
    public class ModelsToEntities : Profile
    {
        public ModelsToEntities()
        {
            CreateMap<BaseViewModel, BaseEntity>();
            CreateMap<OptionModel, OptionKey>();
            CreateMap<OptionValue, OptionSet>();
        }
    }
}