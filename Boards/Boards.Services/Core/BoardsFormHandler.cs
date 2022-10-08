using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Boards.Domain;
using Boards.Domain.Contacts;
using DI;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Services;
using DI.Forms;
using DI.Forms.Requests;
using DI.Forms.Types;
using DI.Services.Handlers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Boards.Services.Core
{
    public abstract class BoardsFormHandler<T> : FormHandlerBase<T> where T : class, IEntity, new()
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

        #region Load view Data
        protected override async Task LoadData(FormSchema schema, long entityId, FormActionResult result)
        {
            var repo = GetRepo<T>();
            var entity = await repo.GetById(entityId, true);
            entity.ThrowIfNull($"Entity not found for given id {entityId}");
            entity.UpdateInitValues(result.InitialValues, schema);
            entity.UpdateHdrValues(result.HdrValues, schema);
            if (schema.Actions.Count > 0)
                await SetActionRules(entity, schema);
            result.SetResult(entity, entity.GetName());
        }
        protected virtual async Task SetActionRules(T entity, FormSchema schema)
        {
            await Task.Delay(0);
        }

        #endregion


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
                    value.Options = rs.Values.Select(x => new SelectItem($"{x.Id}", $"{x.Label}"))
                        .ToList();

            }
        }

        protected async Task<string> GetOpSetLabel(long id)
        {
            var repo = GetRepo<OptionSet>();
            var entity = await repo.GetById(id, false);
            entity.ThrowIfNull($"Entity not found for given id {id}");
            return entity.Label;
        }

        protected long? GetLookupId(IDictionary<string, object> data, string key)
        {
            if (!data.ContainsKey(key) || data[key] == null) return null;
            var ov = data[key].ConvertToOption();
            if (ov == null) return null;
            if (long.TryParse($"{ov.Value}", out var lookupId))
                return lookupId;
            return null;
        }

    }
}