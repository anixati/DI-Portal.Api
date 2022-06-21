using Boards.Domain;
using Boards.Services.Core;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Skills.Forms
{
    public class FormsHandler : BoardsFormHandler<Domain.Contacts.Skill>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaKey => "skill";
    }
}