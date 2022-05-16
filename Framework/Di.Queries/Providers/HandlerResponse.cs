using System.Collections.Generic;
using Di.Qry.Core;

namespace Di.Qry.Providers
{
    public class HandlerResponse : IHandlerResponse
    {
        public HandlerResponse(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public string Key { get; private set; }
        public int Count { get; set; }
        public IEnumerable<IDictionary<string, object>> Data { get; private set; }
        internal void SetResult(int count, IEnumerable<IDictionary<string, object>> data)
        {
            Count = count;
            Data = data;
        }
    }
}