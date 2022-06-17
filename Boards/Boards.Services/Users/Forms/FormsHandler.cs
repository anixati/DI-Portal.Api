using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using DI.Domain.Users;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Users.Forms
{
    public class FormsHandler : BoardsFormHandler<AppUser>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "appuser";
    }
}