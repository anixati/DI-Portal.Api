using Boards.Domain;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Terms.Forms
{
    public class FormsHandler : BoardsFormHandler<Domain.Boards.MinisterTerm>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "ministerterm";
    }
}