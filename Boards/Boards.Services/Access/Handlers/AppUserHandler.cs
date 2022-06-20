using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using DI.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Access.Handlers
{
    public class AppUserHandler : BoardsFormHandler<AppUser>
    {
        public AppUserHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appuser";
    }
}