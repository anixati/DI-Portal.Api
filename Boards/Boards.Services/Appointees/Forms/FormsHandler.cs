using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Services.Core;
using DI;
using DI.Domain.Users;
using DI.Forms.Requests;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointees.Forms
{
    public class FormsHandler : BoardsFormHandler<Appointee>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appointee";
    }


    public class UserRoleHandler : BoardsFormHandler<AppointeeSkill>
    {
        public UserRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appointeeskill";

        protected override async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            var repo = GetRepo<Appointee>();
            var entity = await repo.GetById(entityId, "AppointeeSkills");
            entity.ThrowIfNull($"No valid user found for {entityId}");
            if (entity.AppointeeSkills.Any())
                entity.AppointeeSkills.Clear();
            await SaveAsync();
            entity.AppointeeSkills = selection.Select(x => new AppointeeSkill { AppointeeId = entity.Id, SkillId = x }).ToList();
            await SaveAsync();
            return new FormActionResult();
        }
    }
}