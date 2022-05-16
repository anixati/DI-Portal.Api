using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public class QryOption
    {
        public QryOption() { }

        public QryOption(string code,string name)
        {
            Name = name;
            Code = code;
        }


        public string Code { get; set; }
        public string Name { get; set; }

        public JObject GetConfig()
        {
            return JObject.FromObject(new {name = Name, value = Code});
        }
    }
}