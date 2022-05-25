using System.Collections.Generic;

namespace Di.Qry.Schema
{
    public static class Utils
    {
        internal static Dictionary<string, string> SqlOperatorMap = new()
        {
            {"equals to", "="}, {"not equals to", "!="}, {"contains", "like"}, {"starts with", "like"},
            {"ends with", "like"}, {"is greater than", ">"}, {"is less than", "<"}, {"in", "in"}
        };
    }
}