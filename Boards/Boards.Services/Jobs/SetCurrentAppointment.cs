using Boards.Domain;
using Boards.Domain.Roles;
using DI.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Threading.Tasks;

namespace Boards.Services.Jobs
{
    [DisallowConcurrentExecution]
    public class SetCurrentAppointment : JobBase
    {
        private readonly IBoardsContext _boardsContext;
        public SetCurrentAppointment(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }

        protected override async Task ExecuteTask()
        {
            var date = DateTime.Now;
            var rp = _boardsContext.Repo<BoardAppointment>();
            foreach (var apt in await rp.GetListAsync(x => x.Disabled == false))
            {
                if (apt.StartDate <= date && apt.EndDate > date)
                    apt.IsCurrent = true;
                else
                    apt.IsCurrent = false;
                await rp.UpdateAsync(apt);
                
                Trace($" Updated {apt.Name}");
            }
        }
    }
}
