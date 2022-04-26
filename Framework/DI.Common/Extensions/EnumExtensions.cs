using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace System
{
    public static class EnumExtensions
    {
        public static string ToDesc(this Enum enumeration)
        {
            var attribute = GetText<DescriptionAttribute>(enumeration);
            return attribute.Description;
        }

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

        public static IEnumerable<string> GetDescriptions(this Enum enumeration)
        {
            var list = new List<string>();
            var type = enumeration.GetType();
            foreach (var name in Enum.GetNames(type))
            {
                var attrs = type.GetField(name)?.GetCustomAttributes(typeof(DescriptionAttribute), true);
                if (attrs is not {Length: > 0}) continue;
                if (attrs[0] is DescriptionAttribute ds)
                    list.Add(ds.Description);
            }

            return list;
        }
    }
}