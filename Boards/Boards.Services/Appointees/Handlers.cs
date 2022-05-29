using System.Threading.Tasks;
using Boards.Domain.Contacts;
using DI.Domain.Core;
using DI.Forms.Requests;
using DI.Services.Forms;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Appointees
{
    public abstract class BoardFormHandler<T>: FormActionBase<T> where T : class, IEntity, new()
    {
        protected BoardFormHandler(ILoggerFactory logFactory) : base(logFactory)
        {
        }
    }


    public  class AppointeeCreateHandler : BoardFormHandler<Appointee>
    {
        public AppointeeCreateHandler(ILoggerFactory logFactory) : base(logFactory)
        {
        }

        public override string SchemaName => "CreateAppointee";
        protected override async Task<FormActionResult> Process(Appointee entity)
        {
            var x = entity.FullName;

            await Task.Delay(0);
            return new FormActionResult{ RouteKey = "w"};
        }
    }
}
