using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Portfolios.Forms
{
    public class FormsHandler : BoardsFormHandler<Portfolio>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "portfolio";
    }
}