using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions.Impl;
using DI.Domain.Config;
using DI.Domain.Core;
using DI.Domain.Requests;
using DI.Forms.Core;
using DI.Forms.Requests;
using DI.Forms.Types;
using FastMember;
using Microsoft.Extensions.Logging;

namespace DI.Services.Forms
{
    public abstract class FormHandler<T> : ServiceBase, IFormActionHandler where T : class, IEntity, new()
    {
        protected FormHandler(ILoggerFactory logFactory) : base(logFactory)
        {
        }

        public abstract string SchemaKey { get; }

        #region View Handle
        public async Task<FormActionResult> LoadViewData(FormSchema schema, long entityId)
        {
            var rs = new FormActionResult();
            rs.LoadKeys(schema);
            await LoadData(schema, entityId, rs);
            return rs;
        }
        protected abstract Task LoadData(FormSchema schema, long entityId, FormActionResult result);

        #endregion

        #region Create Handle

        public Task<FormActionResult> CreateEntity(IDictionary<string, object> data, long? entityId)
        {
            var entity = data.CreateEntity<T>();
            OnPreCreate(entity, data);
            return CreateEntity(entity);
        }

        public EntityTypeResponse GetEntityType(EntityTypeRequest request)
        {
            return new EntityTypeResponse {EntityType = typeof(T)};
        }

        public async Task LoadOptions(FormSchema responseSchema)
        {
            var map = responseSchema.CreateOptions();
            await LoadOptionSets(map);
            responseSchema.LoadOptions(map);
        }

        protected abstract Task LoadOptionSets(Dictionary<string, OptionFieldConfig> map);

        protected virtual void OnPreCreate(T entity, IDictionary<string, object> data)
        {
        }
        protected abstract Task<FormActionResult> CreateEntity(T entity); 

        #endregion
    }
}