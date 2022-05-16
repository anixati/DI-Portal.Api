using Boards.Domain.Boards;
using DI.Services.Core;
using DI.WebApi.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Boards.Api.Controllers
{
    [ApiVersionNeutral]
    [Route("portfolios")]
    public class PortfolioController : GenericController<Portfolio, PortfolioViewModel>
    {
        public PortfolioController(ILoggerFactory factory, IServiceContext context) : base(factory, context)
        {
        }
    }
}