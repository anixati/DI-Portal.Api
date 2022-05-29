using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Services
{
    public interface IDataStore<out T> : IDataStore where T : DbContext
    {
        T Db { get; }
    }
}