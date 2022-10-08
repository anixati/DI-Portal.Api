using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Forms.Core;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
{
    public abstract class FormHandlerBase<T> : ServiceBase, IFormActionHandler where T : class, IEntity, new()
    {
        protected FormHandlerBase(ILoggerFactory logFactory) : base(logFactory)
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

        public async Task<FormActionResult> LoadCreateData(FormSchema schema, long? entityId)
        {
            var rs = new FormActionResult();
            await LoadCreateData(schema, entityId, rs);
            return rs;
        }

        protected virtual async Task LoadCreateData(FormSchema schema, long? entityId, FormActionResult result)
        {
            await Task.Delay(0);
        }


        protected abstract Task LoadData(FormSchema schema, long entityId, FormActionResult result);

        #endregion

        #region Create Handle

        public async Task<FormActionResult> CreateEntity(IDictionary<string, object> data, long? entityId)
        {
            var entity = data.CreateEntity<T>();
            await OnPreCreate(entity, data, entityId);
            return await CreateEntity(entity);
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

        protected virtual async Task OnPreCreate(T entity, IDictionary<string, object> data, long? entityId)
        {
            await Task.Delay(0);
        }
        protected abstract Task<FormActionResult> CreateEntity(T entity);

        #endregion

        #region Manage Handle

        public async Task<FormActionResult> ManageEntity(IDictionary<string, object> data, long entityId)
        {
            data.ThrowIfNull($"Input data is null");
            var selection = data.Keys.Select(s => new { rs = long.TryParse(s, out var value), value })
                .Where(p => p.rs)
                .Select(p => p.value).ToList();
            selection.ThrowIfNull($"Input data is null");
            var rs = await CreateIntersection(entityId, selection);
            return rs;
        }

        protected virtual async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            await Task.Delay(0);
            throw new NotImplementedException($"Not implemented");
        }
        public virtual async Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId)
        {
            await Task.Delay(0);
            throw new NotImplementedException($"Not implemented");
        }

        #endregion

    }
}