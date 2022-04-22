using System.Threading.Tasks;

namespace DI.Domain.Services
{
    public interface IAutoNumberStore
    {
        Task<int> GetNext(string key);
    }
}