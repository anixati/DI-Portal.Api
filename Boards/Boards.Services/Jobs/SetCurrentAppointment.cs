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
            var arp = _boardsContext.Repo<BoardAppointment>();
            //var rrp = _boardsContext.Repo<BoardRole>();
            foreach (var apt in await arp.GetListAsync(x => x.Disabled == false))
            {
                if (apt.StartDate <= date && apt.EndDate > date)
                    apt.IsCurrent = true;
                else
                    apt.IsCurrent = false;
                await arp.UpdateAsync(apt);

                //if (apt.IsCurrent.GetValueOrDefault())
                //{

                //    var role = await rrp.FindAsync(x => x.Disabled == false && x.Id == apt.BoardRoleId);
                //    if (role != null)
                //    {
                //        role.IncumbentId = apt.Id;
                //        await rrp.UpdateAsync(role);
                //    }
                //}

                Trace($" Updated {apt.Name}");
            }
        }
    }
}
