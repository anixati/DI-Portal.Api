using AutoMapper;
using Boards.Domain.Boards;

namespace Boards.Services.Maps
{
    public class EntitiesToModels : Profile
    {
        public EntitiesToModels()
        {
            CreateMap<Portfolio, PortfolioViewModel>();
        }
    }
}