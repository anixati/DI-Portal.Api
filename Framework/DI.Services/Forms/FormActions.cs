using System;
using System.Collections.Generic;
using System.Linq;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Forms;
using DI.Forms.Requests;
using DI.Forms.Types;
using FastMember;
using Newtonsoft.Json;

namespace DI.Services.Forms
{
    public static class FormActions
    {

        public static void LoadOptions(this FormSchema schema, Dictionary<string, OptionFieldConfig> map)
        {
            LoadOptions(schema.Fields, map);
        }

        public static void LoadOptions(List<FormField> schemaFields, Dictionary<string, OptionFieldConfig> map)
        {
            foreach (var fd in schemaFields)
            {
                if (fd.FieldType == FormFieldType.PickList && map.ContainsKey(fd.Key))
                    fd.Options = map[fd.Key].Options;
                if (fd.Fields.Any())
                    LoadOptions(fd.Fields, map);
            }
        }

        public static Dictionary<string, OptionFieldConfig> CreateOptions(this FormSchema schema)
        {
            var config = new Dictionary<string, OptionFieldConfig>();
            CreateOptions(schema.Fields, config);
            return config;
        }

        private static void CreateOptions(List<FormField> schemaFields, IDictionary<string, OptionFieldConfig> config)
        {
            foreach (var fd in schemaFields)
            {
                if (fd.FieldType == FormFieldType.PickList)
                    config[$"{fd.Key}"] = new OptionFieldConfig(fd.ViewId);
                if (fd.Fields.Any())
                    CreateOptions(fd.Fields, config);
            }
        }

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
                else if (mi.Type == typeof(int))
                {
                    var rx = (int)accessor[entity, key];
                        data[key] = $"{rx}";

                }
                else if (mi.Type == typeof(DateTime?))
                {
                    var rx = (DateTime?)accessor[entity, key];
                    data[key] = rx.HasValue?$"{rx:o}":"";

                }
                else if (mi.Type == typeof(DateTime))
                {
                    var rx = (DateTime)accessor[entity, key];
                    data[key] = $"{rx:o}";

                }
                else if (mi.Type.IsEnum)
                {
                    data[key] = $"{(int)accessor[entity, key]}";
                }
                else if (mi.Type.IsClass && typeof(IEntity).IsAssignableFrom(mi.Type))
                {
                    var idKey = $"{mi.Name}Id";
                    var idMemType = members.FirstOrDefault(x => string.Compare(x.Name, idKey, StringComparison.OrdinalIgnoreCase) == 0);
                    if (idMemType == null) continue;

                    if (mi.Type == typeof(OptionSet))
                    {
                        var obj = accessor[entity, idKey];
                        if (obj != null)
                            data[key] = $"{obj}";
                    }
                    else
                    {
                        var rv = accessor[entity, idKey];
                        if (value == null) continue;
                        var ls = "GET FROM DB";
                        var vp = accessor[entity, key];
                        if (vp !=  null)
                        {
                            var et = vp as IEntity;
                            ls = et?.GetName();
                        }
                        data[key] = JsonConvert.SerializeObject(new {value=rv, label=ls});
                    }
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
                        accessor[entity, key] = rs;
                }
                else if (mi.Type == typeof(decimal) || mi.Type == typeof(decimal?))
                {
                    if (decimal.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs;
                }
                else if (mi.Type == typeof(DateTime) || mi.Type == typeof(DateTime?))
                {
                    if (DateTime.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs;
                }
                else if (mi.Type.IsEnum)
                {
                    if (int.TryParse($"{value}", out var rs))
                        accessor[entity, key] = rs;
                }
                else if (mi.Type.IsClass && typeof(IEntity).IsAssignableFrom(mi.Type))
                {
                    var idKey = $"{mi.Name}Id";
                    var idMemType = members.FirstOrDefault(x => string.Compare(x.Name, idKey, StringComparison.OrdinalIgnoreCase) == 0);
                    if (idMemType == null) continue;

                    if (mi.Type == typeof(OptionSet))
                    {
                        if (!long.TryParse($"{value}", out var rs)) continue;
                        accessor[entity, idKey] = rs;
                    }
                    else
                    {
                        var ov =value.ConvertToOption();
                        if (ov == null) continue;
                        if (!long.TryParse(ov.Value, out var rs)) continue;
                        accessor[entity, idKey] = rs;
                    }
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