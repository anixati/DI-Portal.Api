using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Reports;
using DI.Security;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Boards.Infrastructure.Domain
{
    public class BoardsContext : IBoardsContext
    {
        private readonly IIdentity _user;
        private readonly IDataStore<BoardsDbContext> _dataStore;
        public BoardsContext(IIdentityProvider provider, IDataStore<BoardsDbContext> dataStore)
        {
            _user = provider.GetIdentity();
            _dataStore = dataStore;
        }

        public IDataStore Store => _dataStore;
        public IIdentity User => _user;
        public IRepository<T> Repo<T>() where T : class, IEntity
        {
            return _dataStore.Repo<T>();
        }
        
        public async Task<List<DashboardItem>> GetDashboardItems(FormattableString iplSql) 
        {
            var rs= await _dataStore.Db.DashboardItems.FromSqlInterpolated(iplSql).ToListAsync();
            return rs;
        }
    }
}