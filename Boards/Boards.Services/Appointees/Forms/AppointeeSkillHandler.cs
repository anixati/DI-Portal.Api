using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Services.Core;
using DI;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointees.Forms
{
    public class AppointeeSkillHandler : BoardsFormHandler<AppointeeSkill>
    {
        public AppointeeSkillHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appointeeskill";

        protected override async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            var repo = GetRepo<Appointee>();
            var entity = await repo.GetById(entityId, "AppointeeSkills");
            entity.ThrowIfNull($"Entity not found for {entityId}");
            if (entity.AppointeeSkills.Any())
                entity.AppointeeSkills.Clear();
            await SaveAsync();
            entity.AppointeeSkills = selection.Select(x => new AppointeeSkill {AppointeeId = entity.Id, SkillId = x})
                .ToList();
            await SaveAsync();
            return new FormActionResult();
        }

        public override async Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId)
        {
            var repo = GetRepo<Appointee>();
            var rs = new FormActionResult();
            var entity = await repo.GetById(entityId, "AppointeeSkills");
            entity.ThrowIfNull($"Entity not found for {entityId}");
            if (entity.AppointeeSkills.Any())
                rs.InitialValues = entity.AppointeeSkills
                    .ToDictionary(o => $"{o.SkillId}", v => "");
            return rs;
        }
    }
}