using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormLoadHandler: IFormActionHandler
    {
        Task<(IDictionary<string, object>, FormEntity)> Execute(FormSchema schema, long entityId);
    }
}