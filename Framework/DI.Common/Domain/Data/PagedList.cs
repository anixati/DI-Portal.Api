using System;
using System.Collections.Generic;
using DI.Core;

namespace DI.Domain.Data
{
    public class PagedList<T> : IPagedList<T> where T : class
    {
        private PagedList(List<T> data, long total, int pageIndex, int pageSize)
        {
            Items = data;
            Total = total;
            PageIndex = pageIndex;
            PageSize = pageSize;
        }

        public PagedList()
        {
            Items = null;
            Total = 0;
            PageIndex = 0;
            PageSize = 50;
        }

        public int PageIndex { get; }
        public int PageSize { get; }
        public int PageCount => (int) Math.Ceiling(Total / (double) PageSize);
        public long Total { get; }
        public List<T> Items { get; }
        public bool HasPrevious => PageIndex > 1;
        public bool HasNext => PageIndex < PageCount;

        public static PagedList<TK> Create<TK>(List<TK> data) where TK : class
        {
            var count = data.Count;
            return new PagedList<TK>(data, count, 1, 20);
        }

        public static PagedList<TK> Create<TK>(List<TK> data, long total, int index, int size) where TK : class
        {
            return new PagedList<TK>(data, total, index, size);
        }

        public static PagedList<TK> Create<TK>(List<TK> data, long total, PageCookie cookie) where TK : class
        {
            return new PagedList<TK>(data, total, cookie.Index, cookie.Size);
        }
    }
}