using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Domain.Roles;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.BoardRoles.Forms
{
    public class FormsHandler : BoardsFormHandler<BoardRole>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        protected override async Task OnPreCreate(BoardRole entity, IDictionary<string, object> data, long? entityId)
        {
            if (!entityId.HasValue)
                throw new Exception($"Board id is required");
            entity.BoardId = entityId.GetValueOrDefault();
            await SetName(entity, data);
        }

        private async Task SetName(BoardRole entity, IDictionary<string, object> data)
        {
            if (data.ContainsKey("Position") && data["Position"] != null)
            {
                if (long.TryParse($"{data["Position"]}", out var posId))
                    entity.Name = await GetOpSetLabel(posId);
            }
        }

        public override string SchemaKey => "boardrole";
    }
}