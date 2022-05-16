using Newtonsoft.Json.Linq;

namespace Di.Qry.Requests
{
    public class ConfigResponse
    {
        public ConfigResponse(JObject data)
        {
            Data = data;
        }

        public JObject Data { get; }
    }
}