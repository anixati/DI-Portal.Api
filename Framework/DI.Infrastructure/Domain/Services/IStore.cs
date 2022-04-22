using Microsoft.EntityFrameworkCore;

namespace DI.Domain.Services
{
    public interface IStore<out T> : IStore where T : DbContext
    {
        T Db { get; }
    }
}