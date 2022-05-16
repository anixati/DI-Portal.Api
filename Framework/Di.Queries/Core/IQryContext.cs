using System.Collections.Generic;

namespace Di.Qry.Core
{
    public interface IQryContext
    {
        string DataSetName { get; }
        string QueryString { get; }
        Dictionary<string, object> Parameters { get; }
    }
}