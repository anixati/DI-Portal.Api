using System;
using System.Threading.Tasks;
using DI.Domain.Core;
using DI.Domain.Data;
using DI.Domain.Queries;
using DI.Domain.Services;

namespace DI.Services.Core
{
    public interface IServiceContext
    {
        Task<IPagedList<TK>> GetListByQry<T, TK>(Action<QryBuilder<T>> builder, bool tracking = false) where T : class, IEntity where TK : class, IViewModel;
       Task<IPagedList<TK>> GetList<T, TK>(IQrySpec<T> spec) where T : class, IEntity where TK : class, IViewModel;
    }
}