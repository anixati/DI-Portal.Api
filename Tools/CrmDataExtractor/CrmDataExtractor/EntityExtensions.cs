using System;
using System.Globalization;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Metadata;

namespace CrmDataExtractor
{
    public static class EntityExtensions
    {
        public static string ToFormattedValue(this object value)
        {
            var valueType = value.GetType();

            if (valueType == typeof(bool)) return (bool) value ? "true" : "false";

            if (valueType == typeof(Guid)) return ((Guid) value).ToString("N");

            if (valueType == typeof(EntityReference))
            {
                return value is EntityReference er ? $"{er.Name}|{er.Id:N}" : "";
            }

            if (valueType == typeof(int)) return $"{((int)value)}";
            if (valueType == typeof(long)) return $"{((long)value)}";
            if (valueType == typeof(decimal)) return $"{((decimal)value)}";

            if (valueType == typeof(string)) return (string) value;

            if (valueType == typeof(DateTime)) return ((DateTime) value).ToString("dd/MM/yyyy hh:mm tt Z");

            if (valueType == typeof(AliasedValue))
            {
                var aliasedValue = (AliasedValue) value;
                return aliasedValue.Value.ToFormattedValue();
            }

            return string.Empty;
        }

        public static string ToString(this Entity entity, string attributeName)
        {
            if (!entity.Contains(attributeName)) return string.Empty;

            var value = entity[attributeName];
            var valueType = value.GetType();

            if (valueType == typeof(OptionSetValue))
            {
                var formattedValue = entity.FormattedValues.Any(e => e.Key == attributeName)
                    ? entity.FormattedValues[attributeName]?.ToString(CultureInfo.CurrentCulture)
                    : null;
                //if (string.IsNullOrEmpty(formattedValue) && metadataService != null)
                //    formattedValue = metadataService.ResolveOptionSetValueLabel(entity, attributeName);
                return formattedValue;
            }

            return value.ToFormattedValue();
        }
        
    }
}