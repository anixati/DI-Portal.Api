using DI.Domain.Core;
using DI.Domain.Options;
using DI.Forms;
using DI.Forms.Requests;
using DI.Forms.Types;
using FastMember;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DI.Services.Handlers
{
    public static class FormActionExtensions
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
            var ls = schema.Fields.Where(x => x.Layout != LayoutType.Header).ToList();
            LoadKeys(ls, rs.InitialValues);
            var hs = schema.Fields.Where(x => x.Layout == LayoutType.Header).ToList();
            LoadKeys(hs, rs.HdrValues);
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

        public static void UpdateHdrValues<T>(this T entity, IDictionary<string, string> data, FormSchema schema)
        {
            var mapper = new EntityMapper<T>(entity);
            foreach (var (key, value) in data)
            {
                var val = mapper.GetValue(key, true);
                if (val == null) continue;
                data[key] = $"{val}";
            }
        }
        public static void UpdateInitValues<T>(this T entity, IDictionary<string, string> data, FormSchema schema)
        {
            var mapper = new EntityMapper<T>(entity);
            foreach (var (key, value) in data)
            {
                var val = mapper.GetValue(key, false);
                if (val == null) continue;
                data[key] = $"{val}";
            }
        }


        public static T CreateEntity<T>(this IDictionary<string, object> data) where T : class,  new()
        {
            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            var entity = new T();
            foreach (var (key, value) in data)
            {
                var nested = key.Contains('.');
                var entKey = key;
                var subKey = string.Empty;
                if (nested)
                {

                    var ix = key.IndexOf('.');
                    subKey = key[(ix + 1)..];
                    entKey = key[..ix];
                }

                var mi = members.FirstOrDefault(x => string.Compare(x.Name, entKey, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null || value == null) continue;

                if (mi.Type.IsClass && typeof(IEntity).IsAssignableFrom(mi.Type))
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
                        var ov = value.ConvertToOption();
                        if (ov == null) continue;
                        if (!long.TryParse(ov.Value, out var rs)) continue;
                        accessor[entity, idKey] = rs;
                    }
                }
                else
                {
                    if (nested)
                    {
                        var nesAccesor = TypeAccessor.Create(mi.Type);
                        var nesProp = accessor[entity, entKey];

                        if (nesProp == null)
                        {
                            nesProp = Activator.CreateInstance(mi.Type);
                            accessor[entity, entKey] = nesProp;
                        }
                        var nesMi = nesAccesor.GetMembers().FirstOrDefault(x => string.Compare(x.Name, subKey, StringComparison.OrdinalIgnoreCase) == 0);
                        if (nesMi == null ) continue;
                        MapValues(nesProp, nesMi.Type, nesAccesor, subKey, value);
                    }
                    else
                    {
                        MapValues(entity,mi.Type,accessor,key,value);
                    }
                }
            }
            return entity;
        }

        private static void MapValues<T>(T entity, Type type, TypeAccessor accessor, string key, object value)
        {

            if (type == typeof(bool) || type == typeof(bool?))
            {
                if (int.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs == 1 ? true : false;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                if (int.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs;
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                if (short.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                if (decimal.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                if (DateTime.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs;
            }
            else if (type.IsEnum)
            {
                if (int.TryParse($"{value}", out var rs))
                    accessor[entity, key] = rs;
            }
            else
            {
                accessor[entity, key] = value;
            }
        }


    }
}