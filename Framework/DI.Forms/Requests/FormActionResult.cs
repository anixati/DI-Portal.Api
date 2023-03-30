using System.Collections.Generic;
using DI.Domain.Core;
using DI.Forms.Core;
using Newtonsoft.Json;

namespace DI.Forms.Requests
{
    public class ValResult
    {
        public string Message { get; set; }
        public string Key { get; set; }
    }

    public class FormActionResult
    {
        public FormActionResult()
        {
            InitialValues = new Dictionary<string, string>();
            HdrValues = new Dictionary<string, string>();
        }

        public FormActionResult(IEntity entity, string title = "") : this()
        {
            Entity = new FormEntity(title, entity);
        }

        public FormEntity Entity { get; set; }
        public List<ValResult> ValResults { get; set; } = new();
        public IDictionary<string, string> InitialValues { get; set; }
        public IDictionary<string, string> HdrValues { get; set; }

        public void SetResult(IEntity entity, string title = "")
        {
            Entity = new FormEntity(title, entity);
        }

        public void SetLookupValue(string key, string label, string value)
        {
            InitialValues[key] = JsonConvert.SerializeObject(new
            {
                value, label
            });
        }
    }
}