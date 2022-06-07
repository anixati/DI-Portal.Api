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
            var entity = await Repository.GetById(request.Id,true);
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


                var mi = members.FirstOrDefault(x =>
                    string.Compare($"/{x.Name}", op.path, StringComparison.OrdinalIgnoreCase) == 0);
                if (mi == null|| op.value ==null) continue;

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
                    patch.Operations.Add(op);
                }

                
            }
            patch.ApplyTo(entity);
            var original = await Repository.GetById(entity.Id, true);
            if(original != null)
                entity.OnPreUpdate(original);
            var result = await Repository.UpdateAsync(entity);
            Commit();
            return new DomainResponse(ResponseCode.Updated, "Patched");
        }
    }
}