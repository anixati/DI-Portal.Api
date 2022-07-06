using System;
using System.Collections.Generic;
using System.Linq;
using Di.Qry.Core;
using DI.Queries;
using Newtonsoft.Json.Linq;

namespace Di.Qry.Schema.Types
{
    public class QryField : IQryField
    {
        public QryField(string alias, string fieldName, QryFieldType qryFieldType, string name = "")
        {
            Alias = alias;
            FieldName = fieldName;
            Name = string.IsNullOrEmpty(name) ? fieldName : name;
            QryFieldType = qryFieldType;
        }

        public QryField(string fieldName, QryFieldType qryFieldType, string name = "")
        {
            FieldName = fieldName;
            Name = string.IsNullOrEmpty(name) ? fieldName : name;
            QryFieldType = qryFieldType;
        }

        public QryFieldType QryFieldType { get; set; }
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

        public bool IsMetaData => QryFieldType == QryFieldType.OptionSet &&
                                  !string.IsNullOrEmpty(ReferenceSchema);

        public bool IsSubQry => Table != null;


        #region Config

        public JObject GetConfig(IQryProvider provider)
        {
            var qryConfig = JObject.FromObject(new {name = Name, type = GetTypeStr(QryFieldType), nullable = Nullable});
            Operators = GetOperators(QryFieldType);
            if (IsMetaData)
                Options = provider.GetQryOptions(ReferenceSchema);
            if (Options != null && Options.Any())
            {
                var qryOptions = new JArray();
                foreach (var qop in Options)
                    qryOptions.Add(qop.GetConfig());
                qryConfig["options"] = qryOptions;
            }

            if (QryFieldType == QryFieldType.Status)
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

        public QryClause Transalate(QryFilter filter)
        {
            if (!filter.HasOperator)
                throw new Exception("Invalid filter! Operator cannot be null");

            if (!Utils.SqlOperatorMap.TryGetValue(filter.Operator, out var opKey))
                throw new Exception($"Operator :{filter.Operator} not allowed!");

            var OpVal = filter.Value;
            if (QryFieldType == QryFieldType.Text)
                OpVal = filter.Operator switch
                {
                    "contains" => $"%{filter.Value}%",
                    "starts with" => $"{filter.Value}%",
                    "ends with" => $"%{filter.Value}",
                    _ => OpVal
                };
            return new QryClause(opKey, OpVal);
        }


        private static string GetTypeStr(QryFieldType qryFieldType)
        {
            switch (qryFieldType)
            {
                case QryFieldType.Text:
                case QryFieldType.Search:
                    return "string";

                case QryFieldType.Numeric:
                    return "number";

                case QryFieldType.Date:
                    return "date";

                case QryFieldType.Bool:
                    return "boolean";

                default:
                    return "category";
            }
        }

        private static string[] GetOperators(QryFieldType qryFieldType)
        {
            return qryFieldType switch
            {
                QryFieldType.Search => new[] {"contains"},
                QryFieldType.Status => new[] {"equals to"},
                QryFieldType.Text => new[] {"equals to", "not equals to", "contains", "starts with", "ends with"},
                QryFieldType.Numeric => new[] {"equals to", "not equals to", "greater than", "is less than"},
                QryFieldType.Date => new[] {"equals to", "not equals to", "greater than", "is less than"},
                QryFieldType.Bool => new[] {"equals to", "not equals to"},
                QryFieldType.OptionSet => new[] {"equals to", "not equals to", "in"},
                _ => null
            };
        }

        #endregion Config
    }
}