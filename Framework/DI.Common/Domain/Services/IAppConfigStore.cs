using System.Threading.Tasks;

namespace DI.Domain.Services
{
    public interface IAppConfigStore
    {
        Task<T> GetObjValue<T>(string key);
        Task<object> SetValue(string key, object value);
        Task<T> SetObjValue<T>(string key, T objValue);
        Task<string> GetValue(string key);
    }
}