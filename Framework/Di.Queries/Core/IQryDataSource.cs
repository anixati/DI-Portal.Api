using System.Collections.Generic;
using System.Threading.Tasks;

namespace Di.Qry.Core
{
    public interface IHandlerResponse
    {
        string Name { get; }
        string Key { get; }
        int Count { get; set; }
        IEnumerable<IDictionary<string, object>> Data { get; }
    }

    public interface IQryDataSource
    {
        Task<List<T>> GetList<T>(IQryContext qContext) where T : class;

        Task<IHandlerResponse> ExecuteQuery(IPagedContext qContext);

        Task<IHandlerResponse> ExecuteQuery(IQryContext qContext);
    }
}