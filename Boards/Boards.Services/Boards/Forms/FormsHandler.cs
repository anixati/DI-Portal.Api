using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Boards.Forms
{
    public class FormsHandler : BoardsFormHandler<Board>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "board";
    }
}