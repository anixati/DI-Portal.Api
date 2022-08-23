using Boards.Domain;
using Boards.Domain.Roles;
using DI.Jobs;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Linq;
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
            var rrp = _boardsContext.Repo<BoardRole>();
            var arp = _boardsContext.Repo<BoardAppointment>();
            Trace($" ------- Starting SetCurrentAppointment ------- ");
            var roles = await rrp.GetListAsync(x => x.Disabled == false, true);
            foreach (var role in roles)
            {

                var appointments = await arp.GetListAsync(x => x.Disabled == false && x.BoardRoleId == role.Id, true);

                if (appointments != null)
                {

                    var roleAppointments = appointments.OrderByDescending(x => x.StartDate).ToList();
                    foreach (var apt in roleAppointments)
                    {
                        if (!apt.EndDate.HasValue)
                        {
                            apt.IsCurrent = true;
                        }
                        else
                        {
                            apt.IsCurrent = apt.StartDate <= date && apt.EndDate > date;
                        }

                    }

                    var endDate = roleAppointments.Max(x => x.EndDate);

                    if (roleAppointments.All(x => x.IsCurrent != true))
                    {
                        foreach (var apt in roleAppointments)
                            Trace($" mem  {apt.Name} {apt.Id} - {apt.StartDate} {apt.EndDate} {apt.IsCurrent}");
                        role.VacantFromDate = endDate ?? date;
                        role.IncumbentId = null;
                    }
                    else
                    {
                        var incAppointment =
                            roleAppointments.FirstOrDefault(x => x.IsCurrent == true && x.EndDate == endDate);
                        if (incAppointment != null)
                        {
                            //role.VacantFromDate = null;
                            role.IncumbentId =incAppointment.AppointeeId;
                            role.IncumbentName = incAppointment.Name;
                            role.IncumbentStartDate = incAppointment.StartDate.ToString("dd MMM yyyy");
                            role.IncumbentEndDate = incAppointment.EndDate.HasValue
                                ? incAppointment.EndDate.Value.ToString("dd MMM yyyy")
                                : "";
                        }

                    }

                    
                    arp.UpdateAsync(roleAppointments.ToArray());
                    await _boardsContext.Store.SaveAsync();
                }
                else
                {
                    role.IncumbentId = null;
                    role.VacantFromDate = date;
                }

                if (role.IncumbentId == null)
                {
                    role.IncumbentName = string.Empty;
                    role.IncumbentStartDate = string.Empty;
                    role.IncumbentEndDate = string.Empty;
                }


                if (role.VacantFromDate.HasValue)
                    Trace($" set role vacant for  {role.Name} {role.Id} - {role.BoardId}");
                await rrp.UpdateAsync(role);

            }
            Trace($" ------- SetCurrentAppointment done ------- ");
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
