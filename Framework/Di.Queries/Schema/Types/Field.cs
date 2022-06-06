using System;
using System.Collections.Generic;
using System.Linq;
using Di.Qry.Core;
using DI.Queries;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Schema.Types
{
    public class Field : IQryField
    {
        public Field(string alias, string fieldName, FieldType fieldType, string name = "")
        {
            Alias = alias;
            FieldName = fieldName;
            Name = string.IsNullOrEmpty(name) ? fieldName : name;
            FieldType = fieldType;
        }

        public Field(string fieldName, FieldType fieldType, string name = "")
        {
            FieldName = fieldName;
            Name = string.IsNullOrEmpty(name) ? fieldName : name;
            FieldType = fieldType;
        }

        public FieldType FieldType { get; set; }
        public Table Table { get; set; }
        public string EntityField { get; set; }

        public string Alias { get; }
        public string FieldName { get; }

        public string QueryKey =>
            string.IsNullOrEmpty(Alias) ? $"{FieldName}".ToLower() : $"{Alias}.{FieldName}".ToLower();

        public string Name { get; set; }
        public bool Nullable { get; set; }
        public string ReferenceSchema { get; set; }
        public List<QryOption> Options { get; set; }
        public string[] Operators { get; set; }

        public bool IsMetaData => FieldType == FieldType.OptionSet &&
                                  !string.IsNullOrEmpty(ReferenceSchema);

        public bool IsSubQry => Table != null;


        #region Config

        public JObject GetConfig(IQryProvider provider)
        {
            var qryConfig = JObject.FromObject(new {name = Name, type = GetTypeStr(FieldType), nullable = Nullable});
            Operators = GetOperators(FieldType);
            if (IsMetaData)
                Options = provider.GetQryOptions(ReferenceSchema);
            if (Options != null && Options.Any())
            {
                var qryOptions = new JArray();
                foreach (var qop in Options)
                    qryOptions.Add(qop.GetConfig());
                qryConfig["options"] = qryOptions;
            }

            if (FieldType == FieldType.Status)
            {
                var qryOptions = new JArray
                {
                    new QryOption("0", "Active").GetConfig(),
                    new QryOption("1", "InActive").GetConfig()
                };
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

        public QryClause Transalate(IQryFilter filter)
        {
            if (!filter.HasOperator)
                throw new Exception("Invalid filter! Operator cannot be null");

            if (!Utils.SqlOperatorMap.TryGetValue(filter.Operator, out var opKey))
                throw new Exception($"Operator :{filter.Operator} not allowed!");

            var OpVal = filter.Value;
            if (FieldType == FieldType.Text)
                OpVal = filter.Operator switch
                {
                    "contains" => $"%{filter.Value}%",
                    "starts with" => $"{filter.Value}%",
                    "ends with" => $"%{filter.Value}",
                    _ => OpVal
                };
            return new QryClause(opKey, OpVal);
        }


        private static string GetTypeStr(FieldType fieldType)
        {
            switch (fieldType)
            {
                case FieldType.Text:
                case FieldType.Search:
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

        private static string[] GetOperators(FieldType fieldType)
        {
            return fieldType switch
            {
                FieldType.Search => new[] {"contains"},
                FieldType.Status => new[] {"equals to"},
                FieldType.Text => new[] {"equals to", "not equals to", "contains", "starts with", "ends with"},
                FieldType.Numeric => new[] {"equals to", "not equals to", "greater than", "is less than"},
                FieldType.Date => new[] {"equals to", "not equals to", "greater than", "is less than"},
                FieldType.Bool => new[] {"equals to", "not equals to"},
                FieldType.OptionSet => new[] {"equals to", "not equals to", "in"},
                _ => null
            };
        }

        #endregion Config
    }
}