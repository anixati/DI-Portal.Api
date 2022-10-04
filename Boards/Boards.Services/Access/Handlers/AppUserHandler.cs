using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using DI.Domain.Users;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

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
    }
}