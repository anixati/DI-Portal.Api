using System.Collections.Generic;
using System.Threading.Tasks;
using DI.Forms.Types;

namespace DI.Forms.Core
{
    public interface IFormProvider
    {
        List<string> GetSchemas();
        FormSchema GetSchema(string schemaKey);
    }
}