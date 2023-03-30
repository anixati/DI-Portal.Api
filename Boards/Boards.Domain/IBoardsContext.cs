using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Reports;
using DI.Security;

namespace Boards.Domain
{
    public interface IBoardsContext
    {
        IIdentity User { get; }
        IDataStore Store { get; }
        IRepository<T> Repo<T>() where T : class, IEntity;
        Task<List<DashboardItem>> GetDashboardItems(FormattableString iplSql);
    }
}