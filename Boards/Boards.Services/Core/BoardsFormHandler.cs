using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boards.Domain;
using DI;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Services;
using DI.Forms.Requests;
using DI.Forms.Types;
using DI.Services.Forms;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Core
{
    public abstract class BoardsFormHandler<T> : FormHandler<T> where T : class, IEntity, new()
    {
        private readonly IBoardsContext _boardsContext;
        protected BoardsFormHandler(ILoggerFactory logFactory, IBoardsContext boardsContext) : base(logFactory)
        {
            _boardsContext = boardsContext;
        }

        #region Repos
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
        #endregion

        protected override async Task LoadData(FormSchema schema, long entityId, FormActionResult result)
        {
            var repo = GetRepo<T>();
            var entity = await repo.GetById(entityId,true);
            entity.ThrowIfNull($"Entity not found for given id {entityId}");
            result.InitialValues.MapFromEntity(entity);
            result.SetResult(entity, entity.GetName());
        }

        protected override async Task<FormActionResult> CreateEntity(T entity)
        {
            var repo = GetRepo<T>();
            var rs = await repo.CreateAsync(entity);
            await SaveAsync();
            return new FormActionResult(rs);
        }

        protected override async Task LoadOptionSets(Dictionary<string, OptionFieldConfig> map)
        {
            var repo = GetRepo<OptionKey>();
            foreach (var (_, value) in map)
            {

                var rs = await repo.Query().Include(x => x.Values).Where(x => x.Code == value.Code)
                    .FirstOrDefaultAsync();
                if (rs != null && rs.Values != null && rs.Values.Any())
                    value.Options = rs.Values.Select(x => new SelectFieldOption($"{x.Value}", $"{x.Label}"))
                        .ToList();

            }
        }
    }
}