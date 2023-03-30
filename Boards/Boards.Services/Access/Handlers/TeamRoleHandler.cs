using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Services.Core;
using DI;
using DI.Domain.Users;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Access.Handlers
{
    public class TeamRoleHandler : BoardsFormHandler<TeamRole>
    {
        public TeamRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "teamrole";

        protected override async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            var usrRepo = GetRepo<AppTeam>();
            var entity = await usrRepo.GetById(entityId, "TeamRoles");
            entity.ThrowIfNull($"No valid user found for {entityId}");
            if (entity.TeamRoles.Any())
                entity.TeamRoles.Clear();
            await SaveAsync();
            entity.TeamRoles = selection.Select(x => new TeamRole {AppTeamId = entity.Id, AppRoleId = x}).ToList();
            await SaveAsync();
            return new FormActionResult();
        }

        public override async Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId)
        {
            var repo = GetRepo<AppTeam>();
            var rs = new FormActionResult();
            var entity = await repo.GetById(entityId, "TeamRoles");
            entity.ThrowIfNull($"Entity not found for {entityId}");
            if (entity.TeamRoles.Any())
                rs.InitialValues = entity.TeamRoles
                    .ToDictionary(o => $"{o.AppRoleId}", v => "");
            return rs;
        }
    }
}