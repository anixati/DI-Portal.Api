using System.Threading.Tasks;
using Boards.Domain;
using Boards.Services.Core;
using DI.Domain.Users;
using DI.Exceptions;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;
using BC = BCrypt.Net.BCrypt;

namespace Boards.Services.Access.Handlers
{
    public class AppUserHandler : BoardsFormHandler<AppUser>
    {
        public AppUserHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appuser";

        protected override async Task SetActionRules(AppUser entity, FormSchema schema)
        {
            schema.WithAction("grant_access", x => { x.Visible = !entity.AccessGranted.HasValue; });
            await Task.Delay(0);
        }

        protected override async Task<FormActionResult> CreateEntity(AppUser entity)
        {
            var repo = GetRepo<AppUser>();
            var uid = entity.UserId.ToLower();
            var usr = await repo.FindAsync(x => x.UserId == uid);
            if (usr != null)
                throw new BuisnessException($"User with user id:{uid} already exists");
            entity.PasswordHash = BC.HashPassword("Welcome2023");
            entity.IsSystem = false;
            entity.AccessRequest = System.DateTime.Now;
            entity.AccessGranted = System.DateTime.Now;
            var rs = await repo.CreateAsync(entity);
            await SaveAsync();
            return new FormActionResult(rs);
        }
    }
}