using System;
using System.Collections.Generic;
using System.Linq;
using DI.Domain.Core;
using DI.Forms.Requests;
using DI.Forms.Types;
using FastMember;

namespace DI.Services.Forms
{
    public static class FormActions
    {
        public static void LoadKeys(this FormActionResult rs, FormSchema schema)
        {
            LoadKeys(schema.Fields, rs.InitialValues);
        }

        private static void LoadKeys(List<FormField> schemaFields, IDictionary<string, string> rvl)
        {
            foreach (var fd in schemaFields)
            {
                if (fd.Layout == LayoutType.Default)
                    rvl.Add($"{fd.Key}", "");
                if (fd.Fields.Any())
                    LoadKeys(fd.Fields, rvl);
            }
        }

        public static void MapFromEntity<T>(this IDictionary<string, string> data, T entity)
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            foreach (var (key, value) in data)
            {
                var mi = members.FirstOrDefault(x =>
                    string.Compare(x.Name, key, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null) continue;


                if (mi.Type == typeof(bool))
                {
                    data[key] = (bool)accessor[entity, key] ? "1" : "0";
                }
                else if (mi.Type == typeof(bool?))
                {
                    var rx = (bool?)accessor[entity, key];
                    if (rx.HasValue)
                        data[key] = rx.GetValueOrDefault() ? "1" : "0";
                    else
                        data[key] = "";
                }
                else if (mi.Type == typeof(int?))
                {
                    var rx = (int?)accessor[entity, key];
                    if (rx.HasValue)
                        data[key] = $"{rx.GetValueOrDefault()}";
                    else
                        data[key] = "";
                }
                else if (mi.Type.IsEnum)
                {
                    data[key] = $"{(int)accessor[entity, key]}";
                }
                else
                {
                    data[key] = $"{accessor[entity, key]}";
                }
            }
        }


        public static T CreateEntity<T>(this IDictionary<string, object> data) where T : class, IEntity, new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            var entity = new T();
            foreach (var (key, value) in data)
            {
                var mi = members.FirstOrDefault(x => string.Compare(x.Name, key, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null || value == null) continue;
                if (mi.Type == typeof(bool) || mi.Type == typeof(bool?))
                {
                    if (int.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs == 1 ? true : false;
                }
                else if (mi.Type == typeof(int) || mi.Type == typeof(int?))
                {
                    if (int.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs == 1 ? true : false;
                }
                else if (mi.Type.IsEnum)
                {
                    if (int.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs;
                }
                else
                {
                    accessor[entity, key] = value;
                }
            }
            return entity;
        }
    }
}