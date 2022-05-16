using AutoMapper;
using DI.Domain.Contacts;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Users;
using DI.Domain.App;
using System.Linq;
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