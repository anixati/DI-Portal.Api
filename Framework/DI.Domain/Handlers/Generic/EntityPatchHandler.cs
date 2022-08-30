using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using DI.Core;
using DI.Domain.Core;
using DI.Domain.Options;
using DI.Domain.Requests;
using DI.Domain.Services;
using DI.Extensions;
using DI.Forms;
using DI.Response;
using FastMember;
using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;
using Newtonsoft.Json;

namespace DI.Domain.Handlers.Generic
{
    public class EntityPatchHandler<T> : EntityHandler<T>, IRequestHandler<PatchEntityRequest<T>, DomainResponse>
        where T : class, IEntity
    {
        public EntityPatchHandler(IDataStore dataStore, IMapper mapper) : base(dataStore, mapper)
        {
        }

        public async Task<DomainResponse> Handle(PatchEntityRequest<T> request, CancellationToken cancellationToken)
        {
            Ensure.That(() => request.Id > 0, "id is required");
            var entity = await Repository.GetById(request.Id, true);
            entity.ThrowIfNull("Record not found or deleted !");

            //check invalid Paths
            var invalidList = entity.GetInvalidPatchPaths();
            if (invalidList.Any())
                if (request.Data.Operations.Any(operation => invalidList.Contains(operation.path)))
                    throw new UnauthorizedAccessException();

            var patch = new JsonPatchDocument();

            var accessor = TypeAccessor.Create(typeof(T));
            var members = accessor.GetMembers();
            foreach (var op in request.Data.Operations)
            {
                var entKey = op.path;
                var nested = op.path.Contains('.');
                if (nested)
                {
                    var ix = op.path.IndexOf('.');
                    entKey = op.path[..ix];
                }

                var mi = members.FirstOrDefault(x =>
                    string.Compare($"/{x.Name}", entKey, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null || op.value == null) continue;

                if (mi.Type.IsClass && typeof(IEntity).IsAssignableFrom(mi.Type))
                {
                    var idKey = $"{mi.Name}Id";
                    var idMemType = members.FirstOrDefault(x => string.Compare(x.Name, idKey, StringComparison.OrdinalIgnoreCase) == 0);
                    if (idMemType == null) continue;

                    if (mi.Type == typeof(OptionSet))
                    {
                        if (!long.TryParse($"{op.value}", out var rs)) continue;
                        patch.Operations.Add(new Operation("replace", $"/{idKey}", "", rs));
                    }
                    else
                    {
                        var ov = op.value.ConvertToOption();
                        if (ov == null) continue;
                        if (!long.TryParse($"{ov.Value}", out var rs)) continue;
                        patch.Operations.Add(new Operation("replace", $"/{idKey}", "", rs));
                    }
                }
                else
                {
                    if (nested)
                    {
                        op.path = op.path.Replace(".", "/");
                        var propName = entKey.Substring(1);
                        var nesProp = accessor[entity, propName];
                        if (nesProp == null)
                        {
                            nesProp = Activator.CreateInstance(mi.Type);
                            accessor[entity, propName] = nesProp;
                        }
                    }
                    else
                    {
                        op.value = MapValues(mi.Type, op.value);
                    }

                    patch.Operations.Add(op);
                }


            }
            patch.ApplyTo(entity);

            var result = await Repository.UpdateAsync(entity);
            if (result != null)
            {
                if (await ((IEntityEvent)result).OnCoreEvent(EntityEvent.Update, Store) is T rs)
                    await Repository.UpdateAsync(rs);
            }
            Commit();
            return new DomainResponse(ResponseCode.Updated, "Patched");
        }



        private static object MapValues(Type type, object value)
        {

            if (type == typeof(bool) || type == typeof(bool?))
            {
                if (int.TryParse($"{value}", out var rs))
                    return rs == 1 ? true : false;
            }
            else if (type == typeof(int) || type == typeof(int?))
            {
                if (int.TryParse($"{value}", out var rs))
                    return rs;
            }
            else if (type == typeof(short) || type == typeof(short?))
            {
                if (short.TryParse($"{value}", out var rs))
                    return rs;
            }
            else if (type == typeof(decimal) || type == typeof(decimal?))
            {
                if (decimal.TryParse($"{value}", out var rs))
                    return rs;
            }
            else if (type == typeof(DateTime) || type == typeof(DateTime?))
            {
                if (DateTime.TryParse($"{value}", out var rs))
                    return rs;
            }
            else if (type.IsEnum)
            {
                if (int.TryParse($"{value}", out var rs))
                    return rs;
            }
            return value;
        }
    }
}