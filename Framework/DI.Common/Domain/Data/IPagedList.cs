using System.Collections.Generic;

namespace DI.Domain.Data
{
    public interface IPagedList<T> where T : class
    {
        int PageIndex { get; }
        int PageSize { get; }
        int PageCount { get; }
        long Total { get; }
        List<T> Items { get; }
        bool HasPrevious { get; }
        bool HasNext { get; }
    }
}