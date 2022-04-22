using DI.Domain.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DI.Domain
{
    public class DbStore<T> : DbStoreBase<T> where T : DbContext
    {
        public DbStore(T context, ILoggerFactory loggerFactory)
            : base(context, loggerFactory)
        {
        }
    }
}