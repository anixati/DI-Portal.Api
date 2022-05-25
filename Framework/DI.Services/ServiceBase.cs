using Microsoft.Extensions.Logging;

namespace DI.Services
{
    public abstract class ServiceBase
    {
        protected ILogger Logger;

        protected ServiceBase(ILoggerFactory logFactory)
        {
            Logger = logFactory.CreateLogger(GetType().Name);
        }

        protected void Trace(string message)
        {
            Logger.LogDebug($"{message}");
        }
    }
}