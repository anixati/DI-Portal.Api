using System;
using Di.Qry.Core;

namespace Di.Qry.Requests
{
    public class QryResponse
    {

        public int PageIndex { get;}
        public int PageSize { get; }
        public int PageCount { get; private set; }
        public long Total { get; private set; }
        public object Items { get; private set; }
        public bool HasPrevious { get; private set; }
        public bool HasNext { get; private set; }
        public QryResponse(PageInfo pageInfo)
        {
            PageIndex = pageInfo.CurrentPage;
            PageSize = pageInfo.PageSize;
            HasPrevious = PageIndex > 1;
        }

        public void SetResult(IHandlerResponse response)
        {
            if (response == null) return;
            Total = response.Count;
            PageCount = PageSize > 0?(int)Math.Ceiling(Total / (double)PageSize):1;
            HasNext = PageIndex < PageCount-1;
            Items = response.Data;
        }
    }
}