using Di.Qry.Core;

namespace Di.Qry.Requests
{
    public class QryResponse
    {
        public QryResponse(PageInfo pageInfo)
        {
            PageInfo = pageInfo;
        }
        public  PageInfo PageInfo { get;  }
        public object Data { get; internal set; }


        public void SetResult(IHandlerResponse response)
        {
            if (response == null) return;
            PageInfo.Total = response.Count;
            Data = response.Data;
        }
    }
}