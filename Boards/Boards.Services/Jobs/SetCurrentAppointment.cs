using System;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Roles;
using DI.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;

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
            var rrp = _boardsContext.Repo<BoardRole>();
            var arp = _boardsContext.Repo<BoardAppointment>();
            Trace(" ------- Starting SetCurrentAppointment ------- ");
            var roles = await rrp.GetListAsync(x => x.Disabled == false, true);
            foreach (var role in roles.OrderBy(x => x.BoardId))
            {
                Trace($"- B:{role.BoardId} R:{role.Id} ");
                var appointments = await arp.GetListAsync(x => x.Disabled == false && x.BoardRoleId == role.Id, true);

                if (appointments != null)
                {
                    var roleAppointments = appointments.OrderByDescending(x => x.StartDate).ToList();
                    foreach (var apt in roleAppointments)
                        if (!apt.EndDate.HasValue)
                            apt.IsCurrent = true;
                        else
                            apt.IsCurrent = apt.StartDate <= date && apt.EndDate > date;

                    var incumbant = roleAppointments.FirstOrDefault(x => x.IsCurrent.GetValueOrDefault() == true);
                    if (incumbant != null)
                    {
                        role.IncumbentId = incumbant.AppointeeId;
                        role.IncumbentName = incumbant.Name;
                        role.IncumbentStartDate = incumbant.StartDate.HasValue
                            ? incumbant.StartDate.Value.ToString("dd MMM yyyy")
                            : "";
                        role.IncumbentEndDate = incumbant.EndDate.HasValue
                            ? incumbant.EndDate.Value.ToString("dd MMM yyyy")
                            : "";
                    }


                    arp.UpdateAsync(roleAppointments.ToArray());
                    await _boardsContext.Store.SaveAsync();
                }
                else
                {
                    role.IncumbentId = null;
                }

                if (role.IncumbentId == null)
                {
                    role.IncumbentName = string.Empty;
                    role.IncumbentStartDate = string.Empty;
                    role.IncumbentEndDate = string.Empty;
                }

                Trace($"Incumb:{role.IncumbentName} ");
                await rrp.UpdateAsync(role);

                //calculate vacant from date
                if (appointments != null && role.IncumbentId == null)
                {
                    var endDate = appointments.Max(x => x.EndDate);
                    if (endDate.HasValue && endDate.Value < DateTime.Now)
                    {
                        Trace($" Adding vacant date for   {role.Name} {role.Id} - {endDate}");
                        role.VacantFromDate = endDate ?? date;
                        role.VacantFromDate = endDate;
                        await rrp.UpdateAsync(role);
                    }
                }
            }

            Trace(" ------- SetCurrentAppointment done ------- ");
            //var rrp = _boardsContext.Repo<BoardRole>();
            //foreach (var apt in await arp.GetListAsync(x => x.Disabled == false))
            //{
            //    if (apt.StartDate <= date && apt.EndDate > date)
            //        apt.IsCurrent = true;
            //    else
            //        apt.IsCurrent = false;
            //    await arp.UpdateAsync(apt);

            //    //if (apt.IsCurrent.GetValueOrDefault())
            //    //{

            //    //    var role = await rrp.FindAsync(x => x.Disabled == false && x.Id == apt.BoardRoleId);
            //    //    if (role != null)
            //    //    {
            //    //        role.IncumbentId = apt.Id;
            //    //        await rrp.UpdateAsync(role);
            //    //    }
            //    //}

            //    Trace($" Updated {apt.Name}");
            //}
        }
    }
}