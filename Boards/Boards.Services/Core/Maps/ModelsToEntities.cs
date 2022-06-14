using AutoMapper;
using Boards.Domain.Boards;

namespace Boards.Services.Core.Maps
{
    public class ModelsToEntities : Profile
    {
        public ModelsToEntities()
        {
            CreateMap<PortfolioViewModel, Portfolio>();
        }
    }
}