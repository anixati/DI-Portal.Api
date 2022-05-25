using AutoMapper;
using Boards.Domain.Boards;

namespace Boards.Services.Maps
{
    public class ModelsToEntities : Profile
    {
        public ModelsToEntities()
        {
            CreateMap<PortfolioViewModel, Portfolio>();
        }
    }
}