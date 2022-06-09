using System.Threading;
using System.Threading.Tasks;
using DI.Domain.Requests;
using DI.Site;
using MediatR;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public class SiteMapHandler : ServiceBase, IRequestHandler<SiteMapRequest, SiteMap>
    {
        public SiteMapHandler(ILoggerFactory logFactory) : base(logFactory)
        {
        }

        public async Task<SiteMap> Handle(SiteMapRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(0);
            return new SiteMap();
        }
    }
}