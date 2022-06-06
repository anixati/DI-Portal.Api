using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Domain.Roles;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.BoardRoles.Forms
{
    public class FormsHandler : BoardsFormHandler<BoardRole>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "boardrole";
    }
}