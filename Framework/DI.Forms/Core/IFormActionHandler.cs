using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Requests;
using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormActionHandler : IActionHandler
    {
        Task<FormActionResult> LoadViewData(FormSchema schema, long entityId);
        Task<FormActionResult> LoadCreateData(FormSchema schema, long? entityId);
        Task<FormActionResult> CreateEntity(IDictionary<string, object> data, long? entityId);
        EntityTypeResponse GetEntityType(EntityTypeRequest request);
        Task LoadOptions(FormSchema responseSchema);
        Task<FormActionResult> ManageEntity(IDictionary<string, object> data, long entityId);
        Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId);
    }
}