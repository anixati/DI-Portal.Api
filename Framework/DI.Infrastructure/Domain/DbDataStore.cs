using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DI.Domain
{
    public class DbDataStore<T> : DbDataStoreBase<T> where T : DbContext
    {
        public DbDataStore(T context, ILoggerFactory loggerFactory)
            : base(context, loggerFactory)
        {
        }
    }
}