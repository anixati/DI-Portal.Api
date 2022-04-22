using System.Collections.Generic;
using System.Threading.Tasks;
using Autofac;
using AutoMapper;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;
using DI.Services.Core;
using DI.Services.Requests;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace DI.WebApi.Controllers
{
    public abstract class EntityController : ServiceController
    {
        protected EntityController(ILoggerFactory loggerFactory, IServiceContext serviceContext) : base(loggerFactory, serviceContext)
        {
        }

        [NonAction]
        protected async Task<IApiResponse> GetEntityList<T, TK>(ListRequest request) where T : class, IEntity where TK : class, IViewModel
        {
            return await ExecuteTask(async (x) =>
            {
                request.ThrowIfNull();
                var qb = QryBuilder<T>.Create(x =>
                {
                });
                return await x.GetList<T, TK>(qb);
            });
        }
    }
}