using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Forms.Core;
using DI.Forms.Requests;
using FastMember;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public abstract class FormActionBase<T> : ServiceBase, IFormActionHandler where T : class, IEntity, new()
    {
        protected FormActionBase(ILoggerFactory logFactory) : base(logFactory)
        {
        }
        public abstract string SchemaName { get; }

        public async Task<FormActionResult> Execute(IDictionary<string, object> data, long? entityId)
        {

            var entity = Convert(data);
            var result = await Process(entity);
            return result;
        }
        protected abstract Task<FormActionResult> Process(T entity);


        private T Convert(IDictionary<string, object> data)
        {

            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            var entity = new T();
            foreach (var (key, value) in data)
            {
                var mi = members.FirstOrDefault(x => string.Compare(x.Name, key, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null || value == null) continue;
                if (mi.Type == typeof(bool) || mi.Type == typeof(Nullable<bool>))
                {
                    if (int.TryParse($"{value}", out int rs))
                        accessor[entity, key] = rs == 1 ? true : false;
                }
                else if (mi.Type.IsEnum)
                {
                    if (int.TryParse($"{value}", out int rs))
                        accessor[entity, key] = rs;
                }
                else
                {
                    accessor[entity, key] = value;
                }
            }
            SetMapping(entity, data);
            return entity;
        }

        //private T Convert(dynamic obj, IDictionary<string, object> data)
        //{
        //    var et = obj as T;
        //    SetMapping(et, data);
        //    return et;
        //}

        protected virtual void SetMapping(T entity, IDictionary<string, object> data)
        {
        }

        //private static dynamic ToObject(IDictionary<string, object> data)
        //{
        //    var exo = new ExpandoObject();
        //    var exoCol = (ICollection<KeyValuePair<string, object>>)exo;
        //    foreach (var keyValuePair in data)
        //        exoCol.Add(keyValuePair);
        //    dynamic eoDynamic = exo;
        //    return eoDynamic;
        //}


    }
}