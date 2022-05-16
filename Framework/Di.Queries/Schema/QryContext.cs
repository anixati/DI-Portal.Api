using System.Collections.Generic;
using System.Linq;
using System.Text;
using Di.Qry.Core;
using SqlKata;

namespace Di.Qry.Schema
{
    public class QryContext : IQryContext
    {
        public string DataSetName { get; set; }
        public string QueryString { get; set; }
        public Dictionary<string, object> Parameters { get; set; } = new Dictionary<string, object>();
        public override string ToString()
        {
            var sbr = new StringBuilder();
            sbr.AppendLine($"DataSet Name :{DataSetName}");
            sbr.AppendLine($"Query String :");
            sbr.AppendLine($"{QueryString}");
            sbr.AppendLine($"Parameters :");
            foreach (var sq in Parameters)
                sbr.AppendLine($"{sq.Key} - {sq.Value}");
            return sbr.ToString();
        }

        public static QryContext Create(string dataSetName, SqlResult result)
        {
            var retVal = new QryContext
            {
                DataSetName = dataSetName,
                QueryString = result.Sql
            };
            if (result.NamedBindings != null)
                retVal.Parameters = result.NamedBindings.ToDictionary(x =>
                    x.Key.Replace(":", ""), p => p.Value);
            return retVal;
        }
    }
}