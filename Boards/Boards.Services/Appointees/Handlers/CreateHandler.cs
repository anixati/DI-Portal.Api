using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using Boards.Services.Core;
using DI.Forms.Requests;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointees.Handlers
{
    public class CreateHandler : BoardsCreateHandler<Appointee>
    {
        public CreateHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }

        public override string SchemaName =>  Constants.Forms.Appointee.Create;
        protected override async Task<FormActionResult> Process(Appointee entity)
        {
            var repo = GetRepo<Appointee>();

            var rs = await repo.CreateAsync(entity);
            await SaveAsync();
            return new FormActionResult { RouteKey = "w", EntityId = rs.Id};
        }
    }
}
