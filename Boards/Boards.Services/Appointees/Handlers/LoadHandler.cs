using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Services.Core;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointees.Handlers
{
    public class LoadHandler : BoardsLoadHandler<Appointee>
    {
        public LoadHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }
        public override string SchemaName => Constants.Forms.Appointee.View;
        protected override async Task Process(FormSchema schema, long entityId, Dictionary<string, object> data)
        {
            var repo = GetRepo<Appointee>();
            var entity = await repo.GetById(entityId);
            UpdateData(entity, data);
            
        }
    }
}