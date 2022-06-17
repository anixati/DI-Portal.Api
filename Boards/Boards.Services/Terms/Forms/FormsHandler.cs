using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Boards;
using Boards.Services.Core;
using DI.Forms.Requests;
using DI.Forms.Types;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Terms.Forms
{
    public class FormsHandler : BoardsFormHandler<Domain.Boards.MinisterTerm>
    {
        public FormsHandler(ILoggerFactory logFactory, IBoardsContext context) : base(logFactory, context)
        {
        }


        protected override async Task LoadCreateData(FormSchema schema, long? entityId, FormActionResult result)
        {
            if (!entityId.HasValue) return;
            var repo = GetRepo<Portfolio>();
            var pf = await repo.GetById(entityId.GetValueOrDefault());
            if (pf != null)
                result.SetLookupValue("Portfolio", $"{pf.Name}", $"{pf.Id}");

        }
        public override string SchemaKey => "ministerterm";
    }
}