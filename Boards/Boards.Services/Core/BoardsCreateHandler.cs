using System.Threading;
using System.Threading.Tasks;
using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Services.Forms;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Core
{
    public abstract class BoardsCreateHandler<T> : FormCreateHandler<T> where T : class, IEntity, new()
    {
        private readonly IBoardsContext _boardsContext;
        protected BoardsCreateHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }

        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return _boardsContext.Store.Repo<TK>();
        }
        protected async Task SaveAsync()
        {
            await  _boardsContext.Store.SaveAsync();
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