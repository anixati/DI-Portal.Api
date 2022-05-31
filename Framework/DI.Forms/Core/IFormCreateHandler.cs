using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Requests;
using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormActionHandler
    {
        string SchemaName { get; }
    }
    public interface IFormCreateHandler: IFormActionHandler
    {
        Task<FormActionResult> Execute(IDictionary<string, object> data, long? entityId);
    }

    public interface IFormLoadHandler: IFormActionHandler
    {
        Task<IDictionary<string, object>> Execute(FormSchema schema, long entityId);
    }
}