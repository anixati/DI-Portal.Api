using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Boards.Domain.Shared;

namespace DataTools
{
    public static class Extentions
    {
        private static T GetText<T>(Enum enumeration) where T : Attribute
        {
            var type = enumeration.GetType();
            var memberInfo = type.GetMember(enumeration.ToString());
            if (!memberInfo.Any())
                throw new ArgumentException($"No public members for the argument '{enumeration}'.");
            var attributes = memberInfo[0].GetCustomAttributes(typeof(T), false);
            if (attributes == null || attributes.Length != 1)
                throw new ArgumentException(
                    $"Can't find an attribute matching '{typeof(T).Name}' for the argument '{enumeration}'");
            return attributes.Single() as T;
        }
        public static string ToDesc(this Enum enumeration)
        {
            var attribute = GetText<DescriptionAttribute>(enumeration);
            return attribute.Description;
        }
        public static string Get(this Dictionary<string,string > value,string key)
        {
            return value .ContainsKey(key) ? value[key].Trim() : "";
        }
        public static string Limit(this string value, int length)
        {
            var rx = value.ReplaceWhitespace("");
            if (!string.IsNullOrEmpty(rx) && rx.Length > length)
            {
                return rx[..length];
            }

            return rx;
        }
        private static readonly Regex Whitespace = new Regex(@"\s+");
        public static string ReplaceWhitespace(this string input, string replacement)
        {
            return Whitespace.Replace(input, replacement);
        }


        public static string GetRefId(this string input){
            if (!input.Contains("|")) return input;
            var rm = input.Split(new char[] {'|'}, StringSplitOptions.RemoveEmptyEntries);
            return rm[1];

        }


        public static DateStateEnum ToDateState(this string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                return value=="TBA" ? DateStateEnum.TBA : (value == "N/A" ? DateStateEnum.NA :DateStateEnum.Date);
            }
            return DateStateEnum.NA;
        }

        public static int? ToInt(this string value)
        {
            if (!string.IsNullOrEmpty(value) && int.TryParse(value, out var ix))
            {
                return ix;
            }
            return null;
        }
        public static decimal? ToDeci(this string value)
        {
            if (!string.IsNullOrEmpty(value) && decimal.TryParse(value, out var ix))
            {
                return ix;
            }
            return null;
        }
        public static DateTime? ToDate(this string value)
        {
            if (!string.IsNullOrEmpty(value) && DateTime.TryParse(value, out var ix))
            {
                return ix;
            }
            return null;
        }

        public static bool IsDisabled(this Dictionary<string, string> value,string key = "statecode")
        {
            if (!value.ContainsKey(key)) return false;
            var v = value[key].Trim();
            return string.Compare(v, "Inactive",StringComparison.OrdinalIgnoreCase) == 0;
        }

    }
}
