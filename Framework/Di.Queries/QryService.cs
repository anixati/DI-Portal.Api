using Microsoft.Extensions.Logging;

namespace Di.Qry
{
    public abstract class QryService
    {
        private readonly ILogger _logger;
        protected QryService(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger(GetType().Name);
        }
        protected void Trace(string msg)
        {
            _logger.LogDebug($"{msg}");
        }
    }
}