using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace DI.Jobs
{
    public static class JobExtensions
    {

        public static void SetupJobs(this IServiceCollection services,IConfiguration config,Func<IEnumerable<Type>> GetTypes)
        {
            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();

                var mi = typeof(JobExtensions).GetMethod(nameof(JobExtensions.AddJob));
                foreach (var type in GetTypes())
                {
                    var gm = mi.MakeGenericMethod(new[] { type });
                    gm.Invoke(null, new object[] { q, config });
                }
                
            });
            services.AddQuartzHostedService(  q => q.WaitForJobsToComplete = true);
        }

        public static IEnumerable<Type> GetJobs(this Assembly assembly)
        {
            return assembly.GetTypes().Where(t => t.IsClass && !t.IsAbstract && t.IsSubclassOf(typeof(JobBase)));
        }

        public static void AddJob<T>(IServiceCollectionQuartzConfigurator quartz, IConfiguration config)  where T : IJob
        {
            var jobName = typeof(T).Name;
            var configKey = $"Jobs:{jobName}";
            var cronSchedule = config[configKey];
            if (string.IsNullOrEmpty(cronSchedule))
                throw new Exception($"No Cron schedule found for job in configuration at {configKey}");
            var jobKey = new JobKey(jobName);
            quartz.AddJob<T>(opts => {
                opts.WithIdentity(jobKey);
                });
            quartz.AddTrigger(opts => opts
                .ForJob(jobKey)
                .WithIdentity(jobName + "-trigger")
                .WithCronSchedule(cronSchedule)); // use the schedule from configuration
        }

    }
}
