using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Domain.Roles;
using Boards.Services.Core;
using DI;
using DI.Forms;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointments.Forms
{
    public class FormsHandler : BoardsFormHandler<BoardAppointment>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        protected override async Task OnPreCreate(BoardAppointment entity, IDictionary<string, object> data, long? entityId)
        {
            if (!entityId.HasValue)
                throw new Exception($"Board role id is required");
            var repo = GetRepo<BoardRole>();
            var brl = await repo.GetById(entityId.GetValueOrDefault());
            brl.ThrowIfNull($"No board role found for given id:{entityId}");
            entity.BoardRoleId = brl.Id;
            entity.BoardId = brl.BoardId;
            await SetName(entity, data);
        }

        private async Task SetName(BoardAppointment entity, IDictionary<string, object> data)
        {
            var lid = GetLookupId(data, "Appointee");
            if (lid.HasValue)
            {
                var repo = GetRepo<Appointee>();
                var ape = await repo.GetById(lid.GetValueOrDefault());
                ape.ThrowIfNull($"No entity found for given id");
                entity.Name = ape.FullName;

            }
        }

        public override string SchemaKey => "boardappointment";
    }
}