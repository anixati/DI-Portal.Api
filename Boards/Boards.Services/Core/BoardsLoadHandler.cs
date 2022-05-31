using Boards.Domain;
using DI.Domain.Core;
using DI.Domain.Services;
using DI.Services.Forms;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Core
{
    public abstract class BoardsLoadHandler<T> : FormLoadHandler<T> where T : class, IEntity, new()
    {
        private readonly IBoardsContext _boardsContext;
        protected BoardsLoadHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }
        protected IRepository<TK> GetRepo<TK>() where TK : class, IEntity
        {
            return _boardsContext.Store.Repo<TK>();
        }
    }
}