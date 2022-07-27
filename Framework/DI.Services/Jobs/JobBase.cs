using DI.Services;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace DI.Jobs
{
    public abstract class JobBase : ServiceBase, IJob
    {
        protected JobBase(ILoggerFactory logFactory) : base(logFactory)
        {
            
        }

        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var job = GetType().Name;
                Trace($"Starting {job} Job");
                await ExecuteTask();
                Trace($"Completed {job} Job");
            }
            catch (Exception ex)
            {
                HandleError(ex);
            }
            //return Task.CompletedTask;
        }

        protected abstract Task ExecuteTask();
    }
}
