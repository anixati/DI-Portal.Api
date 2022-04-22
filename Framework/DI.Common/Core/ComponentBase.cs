using System;
using Microsoft.Extensions.Logging;

namespace DI.Core
{
    public abstract class ComponentBase
    {
        protected ComponentBase(ILoggerFactory loggerFactory)
        {
            //TODO: fix this 
            Log = loggerFactory.CreateLogger(GetType().Name);
        }

        protected ILogger Log { get; }

        protected void LogInfo(string msg)
        {
            Log.LogInformation(msg);
        }

        protected void Trace(Func<string> traceMsg)
        {
            if (Log.IsEnabled(LogLevel.Debug))
                Log.LogDebug(traceMsg());
        }
    }
}