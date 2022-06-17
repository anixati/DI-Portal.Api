using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using DI.Attributes;
using DI.Domain.Core;

namespace DI.Extensions
{
    public static class ReflectionExtensions
    {
        public static List<string> GetInvalidPatchPaths<T>(this T entity) where T : IEntity
        {
            var rv = new List<string>();
            var properties = typeof(T)
                .GetProperties(BindingFlags.Instance | BindingFlags.Public);
            foreach (var p in properties)
            {
                if (!p.CanWrite)
                    rv.Add($"/{p.Name}");
                if (p.GetCustomAttributes(typeof(NoPatchAttribute), true).Any()) rv.Add($"/{p.Name}");
            }

            return rv;
        }

        
 
    }
}