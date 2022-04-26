using System.Linq;
using DI.Core;
using DI.Domain.Core;

namespace DI.Domain.Queries
{
    public interface IQrySpec<T> where T : class, IEntity
    {
        bool Tracking { get; }
        bool IsPaged { get; }
        int? Skip { get; }
        int? Take { get; }
        PageCookie GetPageCookie();
        IQueryable<T> Build(IQueryable<T> inputQry);
    }
}