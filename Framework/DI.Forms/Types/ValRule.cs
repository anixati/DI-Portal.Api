using System.Collections.Generic;
using Newtonsoft.Json;

namespace DI.Forms.Types
{
    public class ValRule
    {
        public enum RuleType
        {
            Required = 0,
            Email,
            Phone,
            Url,
            Date,
            Regex,
            Min,
            Max
        }

        public ValRule(RuleType type)
        {
            Rule = type;
            Type = type.ToString().ToLower();
            Data = new List<object>();
        }

        public string Type { get; }

        [JsonIgnore] public RuleType Rule { get; }

        public List<object> Data { get; set; }

        public static ValRule Required(string message)
        {
            return Create(RuleType.Required, message);
        }

        public static ValRule Email(string message)
        {
            return Create(RuleType.Email, message);
        }

        public static ValRule Phone(string message)
        {
            return Create(RuleType.Phone, message);
        }

        public static ValRule Date(string message)
        {
            return Create(RuleType.Date, message);
        }

        public static ValRule Hyperlink(string message)
        {
            return Create(RuleType.Url, message);
        }

        public static ValRule Regex(params object[] options)
        {
            return Create(RuleType.Regex, options);
        }

        public static ValRule Min(params object[] options)
        {
            return Create(RuleType.Min, options);
        }

        public static ValRule Max(params object[] options)
        {
            return Create(RuleType.Max, options);
        }

        public static ValRule Create(RuleType type, params object[] options)
        {
            var rl = new ValRule(type);
            foreach (var option in options)
                rl.Data.Add(option);
            return rl;
        }
    }
}