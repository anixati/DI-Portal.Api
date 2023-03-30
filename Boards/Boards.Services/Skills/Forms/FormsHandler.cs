using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Skills.Forms
{
    public class FormsHandler : BoardsFormHandler<Skill>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "skill";
    }
}