using AutoMapper;
using Boards.Domain.Boards;
using DI.Domain.Contacts;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;

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