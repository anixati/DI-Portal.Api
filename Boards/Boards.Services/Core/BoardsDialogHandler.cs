using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Forms.Requests;
using DI.Services.Handlers;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace Boards.Services.Core
{
    public abstract class BoardsDialogHandler<T> : DialogHandlerBase<T> where T : class, IViewModel, new()
    {
        private readonly IBoardsContext _boardsContext;
        protected BoardsDialogHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }

        public override async Task Initialise(DialogSchemaResponse response, long? entityId)
        {
            await Task.Delay(0);
        }
        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return _boardsContext.Store.Repo<TK>();
        }
        protected async Task SaveAsync()
        {
            await _boardsContext.Store.SaveAsync();
        }

        protected void Commit()
        {
            _boardsContext.Store.Commit();
        }

        protected void Rollback()
        {
            _boardsContext.Store.Rollback();
        }
    }
}