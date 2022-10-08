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
    public class UserRoleHandler : BoardsFormHandler<UserRole>
    {
        public UserRoleHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "userrole";

        protected override async Task<FormActionResult> CreateIntersection(long entityId, List<long> selection)
        {
            var usrRepo = GetRepo<AppUser>();
            var user = await usrRepo.GetById(entityId, "UserRoles");
            user.ThrowIfNull($"No valid user found for {entityId}");
            if (user.UserRoles.Any())
                user.UserRoles.Clear();
            await SaveAsync();
            user.UserRoles = selection.Select(x => new UserRole {AppUserId = user.Id, AppRoleId = x}).ToList();
            await SaveAsync();
            return new FormActionResult();
        }


        public override async Task<FormActionResult> LoadSelectedData(FormSchema schema, long entityId)
        {
            var repo = GetRepo<AppUser>();
            var rs = new FormActionResult();
            var entity = await repo.GetById(entityId, "UserRoles");
            entity.ThrowIfNull($"Entity not found for {entityId}");
            if (entity.UserRoles.Any())
            {
                rs.InitialValues = entity.UserRoles
                     .ToDictionary(o => $"{o.AppRoleId}", v => "");
            }
            return rs;
        }
    }
}