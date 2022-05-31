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
        public async Task<IDictionary<string, object>> Execute(FormSchema schema, long entityId)
        {
            var rvl = new Dictionary<string, object>();
            LoadKeys(schema.Fields, rvl);
            await Process(schema, entityId,rvl);
            return rvl;
        }
        protected abstract Task Process(FormSchema schema, long entityId, Dictionary<string, object> data);
        private static void LoadKeys(List<FormField> schemaFields, Dictionary<string, object> rvl)
        {
            foreach (var fd in schemaFields)
            {
                if (fd.Layout == LayoutType.Default)
                    rvl.Add($"{fd.Key}", null);
                if (fd.Fields.Any())
                    LoadKeys(fd.Fields, rvl);
            }
        }

        protected IDictionary<string, object> UpdateData(T entity, IDictionary<string, object> data)
        {

            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            foreach (var (key, value) in data)
            {
                var mi = members.FirstOrDefault(x =>
                    string.Compare(x.Name, key, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null ) continue;
                

                if (mi.Type == typeof(bool))
                {
                        data[key]  = (bool)accessor[entity, key]? 1 : 0;
                }
                else if (mi.Type == typeof(bool?))
                {
                    var rx = (bool?)accessor[entity, key];
                    if(rx.HasValue)
                    data[key] = rx.GetValueOrDefault() ?1 : 0;
                }
                else
                {
                    data[key] = accessor[entity, key];
                }


               
                //else if (mi.Type == typeof(int) || mi.Type == typeof(int?))
                //{
                //    if (int.TryParse($"{value}", out var rs))
                //        accessor[entity, key] = rs == 1 ? true : false;
                //}
                //else if (mi.Type.IsEnum)
                //{
                //    if (int.TryParse($"{value}", out var rs))
                //        accessor[entity, key] = rs;
                //}
                //else
                //{
                //    accessor[entity, key] = value;
                //}

            }

            return data;
        }
    }
}