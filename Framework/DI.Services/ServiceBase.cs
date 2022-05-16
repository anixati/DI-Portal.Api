using Microsoft.Extensions.Logging;

namespace DI.Services
{
    public abstract class ServiceBase
    {
        protected ILogger Logger;

        protected ServiceBase(ILoggerFactory logFactory)
        {
            Logger = logFactory.CreateLogger(this.GetType().Name);
        }
    }
}