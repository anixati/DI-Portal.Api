using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using DI.Actions;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Requests;
using DI.Services.Core;
using DI.Services.Requests;
using DI.WebApi.Controllers;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Core
{
    public abstract class EntityController : ServiceController
    {
        protected EntityController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory,
            serviceContext)
        {
        }

        protected async Task<IApiResponse> Create<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel
        {
            var result = await ExecuteTask(async x => await x.Create(model, change));
            return result;
        }

        protected async Task<IApiResponse> Update<T, TK>(TK model, Action<T> change = null)
            where T : class, IEntity where TK : class, IViewModel
        {
            var result = await ExecuteTask(async x => await x.Update(model, change));
            return result;
        }

        protected async Task<IApiResponse> ChangeStatus<T>(SetStatusAction action)
            where T : class, IEntity
        {
            var result = await ExecuteTask(async x => await x.ChangeStatus<T>(action));
            return result;
        }


        protected async Task<IApiResponse> PatchEntity<T>(long Id, JsonPatchDocument document)
            where T : class, IEntity
        {
            var request = new PatchEntityRequest<T>(Id, document);
            var result = await ExecuteTask(async x => await x.PatchEntity(request));
            return result;
        }

        protected async Task<IApiResponse> GetEntityById<T, TK>(long entityId)
            where T : class, IEntity where TK : class, IViewModel
        {
            var result = await ExecuteTask(async x => await x.GetById<T, TK>(entityId));
            return result;
        }

        protected async Task<IApiResponse> GetList<T, TK>() where T : class, IEntity where TK : class, IViewModel
        {
            var result = await GetList<T, TK>(new ListRequest());
            return result;
        }

        protected async Task<IApiResponse> GetList<T, TK>(ListRequest request,
            Expression<Func<T, bool>> expression = null)
            where T : class, IEntity where TK : class, IViewModel
        {
            var result = await ExecuteTask(async x =>
            {
                return await x.GetListByQry<T, TK>(qb =>
                {
                    qb.SetPaging(request.GetPageCookie());
                    if (expression != null)
                        qb.Where(expression);
                });
            });
            return result;
        }
    }
}