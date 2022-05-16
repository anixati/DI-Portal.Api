using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public class QryOption
    {
        public string Code { get; set; }
        public string Name { get; set; }

        public JObject GetConfig()
        {
            return JObject.FromObject(new {name = Name, value = Code});
        }
    }
}