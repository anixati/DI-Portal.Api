using Boards.Domain;
using Boards.Services.Core;
using DI.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Access.Handlers
{
    public class AppTeamHandler : BoardsFormHandler<AppTeam>
    {
        public AppTeamHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appteam";
    }
}