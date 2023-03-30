using System.Threading.Tasks;
using DI.Forms.Requests;

namespace DI.Forms.Core
{
    public interface IDialogHandler : IActionHandler
    {
        Task Initialise(DialogSchemaResponse response, long? entityId);
        Task<DialogActionResponse> Execute(DialogActionRequest request, long? entityId);
    }
}