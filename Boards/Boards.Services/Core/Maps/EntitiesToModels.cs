using AutoMapper;
using Boards.Domain.Boards;

namespace Boards.Services.Core.Maps
{
    public class EntitiesToModels : Profile
    {
        public EntitiesToModels()
        {
            CreateMap<Portfolio, PortfolioViewModel>();
        }
    }
}