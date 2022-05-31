using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormLoadHandler: IFormActionHandler
    {
        Task<(IDictionary<string, string>, FormEntity)> Execute(FormSchema schema, long entityId);
    }
}