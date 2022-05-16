using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Core
{
    public class Field
    {
        public Field(string alias, string fieldName, FieldType fieldType, string name = "")
        {
            Alias = alias;
            FieldName = fieldName;
            Name = string.IsNullOrEmpty(name) ? fieldName : name;
            FieldType = fieldType;
        }

        public string Alias { get; }
        public string FieldName { get; }
        public string QueryKey => $"{Alias}.{FieldName}".ToLower();
        public string Name { get; set; }
        public bool Nullable { get; set; }
        public FieldType FieldType { get; set; }
        public string RefDataId { get; set; }
        public List<QryOption> Options { get; set; }
        public string[] Operators { get; set; }


        public bool IsMetaData => FieldType == FieldType.OptionSet &&
                                  !string.IsNullOrEmpty(RefDataId);

        #region Config

        public JObject GetConfig()
        {
            var qryConfig = JObject.FromObject(new {name = Name, type = GetTypeStr(), nullable = Nullable});
            if (Options != null && Options.Any())
            {
                var qryOptions = new JArray();
                foreach (var qop in Options)
                    qryOptions.Add(qop.GetConfig());
                qryConfig["options"] = qryOptions;
            }

            if (Operators != null && Operators.Any())
            {
                var jopList = new JArray();
                foreach (var op in Operators)
                    jopList.Add(op);
                qryConfig["operators"] = jopList;
            }

            return qryConfig;
        }

        private string GetTypeStr()
        {
            switch (FieldType)
            {
                case FieldType.Text:
                    return "string";

                case FieldType.Numeric:
                    return "number";

                case FieldType.Date:
                    return "date";

                case FieldType.Bool:
                    return "boolean";

                default:
                    return "category";
            }
        }

        #endregion Config
    }
}