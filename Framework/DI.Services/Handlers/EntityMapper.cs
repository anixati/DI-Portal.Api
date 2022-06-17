using System;
using System.Linq;
using DI.Domain.Core;
using DI.Domain.Options;
using FastMember;
using Newtonsoft.Json;

namespace DI.Services.Handlers
{
    public class EntityMapper<T>
    {
        private readonly TypeAccessor _accessor;
        private readonly MemberSet _members;
        private readonly T _entity;

        public EntityMapper(T entity)
        {
            _entity = entity;
            _accessor = TypeAccessor.Create(typeof(T));
            _members = _accessor.GetMembers();
        }

        public object GetValue(string propName)
        {
            var mi = _members.FirstOrDefault(x =>
                string.Compare(x.Name, propName, StringComparison.OrdinalIgnoreCase) == 0);
            if (mi == null) return null;
            if (mi.Type == typeof(bool))
            {
                return (bool)_accessor[_entity, propName] ? "1" : "0";
            }
            else if (mi.Type == typeof(bool?))
            {
                var rx = (bool?)_accessor[_entity, propName];
                if (rx.HasValue)
                    return rx.GetValueOrDefault() ? "1" : "0";
                else
                    return "";
            }
            else if (mi.Type == typeof(int?))
            {
                var rx = (int?)_accessor[_entity, propName];
                if (rx.HasValue)
                    return $"{rx.GetValueOrDefault()}";
                else
                    return "";
            }
            else if (mi.Type == typeof(int))
            {
                var rx = (int)_accessor[_entity, propName];
                return $"{rx}";

            }
            else if (mi.Type == typeof(DateTime?))
            {
                var rx = (DateTime?)_accessor[_entity, propName];
                return rx.HasValue ? $"{rx:o}" : "";

            }
            else if (mi.Type == typeof(DateTime))
            {
                var rx = (DateTime)_accessor[_entity, propName];
                return $"{rx:o}";

            }
            else if (mi.Type.IsEnum)
            {
                return $"{(int)_accessor[_entity, propName]}";
            }
            else if (mi.Type.IsClass && typeof(IEntity).IsAssignableFrom(mi.Type))
            {
                var idKey = $"{mi.Name}Id";
                var idMemType = _members.FirstOrDefault(x =>
                    string.Compare(x.Name, idKey, StringComparison.OrdinalIgnoreCase) == 0);
                if (idMemType == null) return null;

                if (mi.Type == typeof(OptionSet))
                {
                    var obj = _accessor[_entity, idKey];
                    if (obj != null)
                        return $"{obj}";
                }
                else
                {
                    var rv = _accessor[_entity, idKey];
                    if (rv == null) return null;
                    var ls = "";
                    var vp = _accessor[_entity, propName];
                    if (vp == null) return null;
                    var et = vp as IEntity;
                    if (et == null) return null;
                    ls = et.GetName();
                    return JsonConvert.SerializeObject(new { value = rv, label = ls });
                }
            }
            return $"{_accessor[_entity, propName]}";
        }
    }
}