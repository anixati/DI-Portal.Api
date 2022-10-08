using DI.Domain.Core;
using DI.Exceptions;
using DI.Forms.Core;
using DI.Forms.Requests;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DI.Services.Handlers
{
    public abstract class DialogHandlerBase<T> : ServiceBase, IDialogHandler where T : class, IViewModel, new()
    {
        protected DialogHandlerBase(ILoggerFactory logFactory) : base(logFactory)
        {
        }

        public abstract string SchemaKey { get; }

        public async Task<DialogActionResponse> Execute(DialogActionRequest request, long? entityId)
        {
            var rs = new DialogActionResponse();

            try
            {
                if (request == null || request.Data == null || request.Data.Count == 0) throw new BuisnessException($"Invalid request");
                var model = request.Data.CreateEntity<T>();
                await OnExecute(model, request.Data, request.EntityId);
                rs.Result = "Done!";
            }
            catch (Exception ex)
            {
                rs.Failed = true;
                rs.Result = ex.Message;
            }

            return rs;
        }

        public abstract Task Initialise(DialogSchemaResponse response, long? entityId);

        protected virtual async Task OnExecute(T model, IDictionary<string, object> data, long entityId)
        {
            await Task.Delay(0);
        }
    }
}