using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions.Impl;
using DI.Domain.Core;
using DI.Forms.Core;
using DI.Forms.Types;
using FastMember;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public abstract class FormLoadHandler<T> : ServiceBase, IFormLoadHandler where T : class, IEntity, new()
    {
        protected FormLoadHandler(ILoggerFactory logFactory) : base(logFactory)
        {
        }

        public abstract string SchemaName { get; }
        public async Task<(IDictionary<string, string>, FormEntity)> Execute(FormSchema schema, long entityId)
        {
            var rvl = new Dictionary<string, string>();
            LoadKeys(schema.Fields, rvl);
            var entity = await Process(schema, entityId, rvl);
            return (rvl, entity);
        }
        protected abstract Task<FormEntity> Process(FormSchema schema, long entityId, Dictionary<string, string> data);
        private static void LoadKeys(List<FormField> schemaFields, Dictionary<string, string> rvl)
        {
            foreach (var fd in schemaFields)
            {
                if (fd.Layout == LayoutType.Default)
                    rvl.Add($"{fd.Key}", "");
                if (fd.Fields.Any())
                    LoadKeys(fd.Fields, rvl);
            }
        }

        protected IDictionary<string, string> UpdateData(T entity, IDictionary<string, string> data)
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
                else
                {
                    data[key] = $"{accessor[entity, key]}";
                }



            }

            return data;
        }
    }
}