using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Requests;

namespace DI.Forms.Core
{
    public interface IFormActionHandler
    {
        string SchemaName { get; }
        Task<FormActionResult> Execute(IDictionary<string, object> data, long? entityId);
    }
}