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
        private readonly ISiteMapProvider _provider;

        public SiteMapHandler(ILoggerFactory logFactory, ISiteMapProvider provider) : base(logFactory)
        {
            _provider = provider;
        }

        public async Task<SiteMap> Handle(SiteMapRequest request, CancellationToken cancellationToken)
        {
            var rs = await _provider.Create();
            return rs;
        }
    }
}