using Microsoft.Extensions.Logging;

namespace DI.Forms.Core
{
    public abstract class FormServiceBase
    {
        private readonly ILogger _logger;

        protected FormServiceBase(ILoggerFactory logFactory)
        {
            _logger = logFactory.CreateLogger(GetType().Name);
        }

        protected void Trace(string msg)
        {
            _logger.LogDebug($"{msg}");
        }
    }
}