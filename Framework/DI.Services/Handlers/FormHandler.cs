using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Forms.Core;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace DI.Services.Handlers
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
    }
}