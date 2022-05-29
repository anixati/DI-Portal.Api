using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Forms.Core;
using DI.Forms.Requests;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public abstract class FormActionBase<T> : ServiceBase, IFormActionHandler where T:class,IEntity,new()
    {
        protected FormActionBase(ILoggerFactory logFactory) : base(logFactory)
        {
        }
        public abstract string SchemaName { get; }
        
        public async Task<FormActionResult> Execute(IDictionary<string, object> data, long? entityId)
        {
            var entity  = Convert(ToObject(data),data);
            var result = await Process(entity);
            return result;
        }
        protected abstract Task<FormActionResult> Process(T entity);
        private  T Convert(dynamic obj, IDictionary<string, object> data)
        {
            var et=  obj as T;
            Finalise(et, data);
            return et;
        }

        protected virtual void Finalise(T et, IDictionary<string, object> data)
        {
        }

        private static dynamic ToObject(IDictionary<string, object> data)
        {
            var exo = new ExpandoObject();
            var exoCol = (ICollection<KeyValuePair<string, object>>)exo;
            foreach (var keyValuePair in data)
                exoCol.Add(keyValuePair);
            dynamic eoDynamic = exo;
            return eoDynamic;
        }
        

    }
}