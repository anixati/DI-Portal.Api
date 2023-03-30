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
    public class TeamUserHandler : BoardsFormHandler<TeamUser>
    {
        public TeamUserHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "teamuser";

        protected override async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            var usrRepo = GetRepo<AppUser>();
            var user = await usrRepo.GetById(entityId, "UserTeams");
            user.ThrowIfNull($"No valid user found for {entityId}");
            if (user.UserTeams.Any())
                user.UserTeams.Clear();
            await SaveAsync();
            user.UserTeams = selection.Select(x => new TeamUser {AppUserId = user.Id, AppTeamId = x}).ToList();
            await SaveAsync();
            return new FormActionResult();
        }

        public override async Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId)
        {
            var repo = GetRepo<AppUser>();
            var rs = new FormActionResult();
            var entity = await repo.GetById(entityId, "UserTeams");
            entity.ThrowIfNull($"Entity not found for {entityId}");
            if (entity.UserTeams.Any())
                rs.InitialValues = entity.UserTeams
                    .ToDictionary(o => $"{o.AppTeamId}", v => "");
            return rs;
        }
    }
}