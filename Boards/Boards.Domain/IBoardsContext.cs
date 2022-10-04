using DI.Domain.Core;
using DI.Domain.Services;
using DI.Reports;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boards.Domain
{
    public interface IBoardsContext
    {
        IDataStore Store { get; }
        IRepository<T> Repo<T>() where T : class, IEntity;
        Task<List<DashboardItem>> GetDashboardItems(FormattableString iplSql);
    }
}