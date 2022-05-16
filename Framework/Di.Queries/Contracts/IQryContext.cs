using System.Collections.Generic;

namespace Di.Qry.Contracts
{
    public interface IQryContext
    {
        string SqlString { get; }
        Dictionary<string, object> Parameters { get; }
    }
}