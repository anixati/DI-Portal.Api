using DI.Forms.Requests;
using System.Threading.Tasks;

namespace DI.Forms.Core
{
    public interface IDialogHandler : IActionHandler
    {
        Task Initialise(DialogSchemaResponse response, long? entityId);
        Task<DialogActionResponse> Execute(DialogActionRequest request, long? entityId);
    }
}