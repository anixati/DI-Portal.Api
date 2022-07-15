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

        private static object GetValue(TypeAccessor accessor,object entity,Type type,string propName)
        {
            if (type == typeof(bool))
            {
                return (bool)accessor[entity, propName] ? "1" : "0";
            }
            else if (type == typeof(bool?))
            {
                var rx = (bool?)accessor[entity, propName];
                return rx.HasValue ? rx.GetValueOrDefault() ? "1" : "0" : "";
            }
            else if (type == typeof(int?))
            {
                var rx = (int?)accessor[entity, propName];
                return rx.HasValue ? $"{rx.GetValueOrDefault()}" : "";
            }
            else if (type == typeof(int))
            {
                var rx = (int)accessor[entity, propName];
                return $"{rx}";

            }
            else if (type == typeof(DateTime?))
            {
                var rx = (DateTime?)accessor[entity, propName];
                return rx.HasValue ? $"{rx:o}" : "";

            }
            else if (type == typeof(DateTime))
            {
                var rx = (DateTime)accessor[entity, propName];
                return $"{rx:o}";

            }
            else if (type.IsEnum)
            {
                return $"{(int)accessor[entity, propName]}";
            }
            else
            {
                return $"{accessor[entity, propName]}";
            }
        }


        public object GetValue(string propName)
        {
            var nested = propName.Contains('.');
            var entKey = propName;
            var subKey = string.Empty;
            if (nested)
            {
                var ix = propName.IndexOf('.');
                entKey = propName[..ix];
                subKey = propName[(ix + 1)..];
            }

            var mi = _members.FirstOrDefault(x =>
                string.Compare(x.Name, entKey, StringComparison.OrdinalIgnoreCase) == 0);
            if (mi == null) return null;

            if (typeof(IEntity).IsAssignableFrom(mi.Type))
            {
                return GetRefEntityValue(mi, propName);
            }

            if (nested)
            {
                var acc = TypeAccessor.Create(mi.Type);

                var nesMi = acc.GetMembers().FirstOrDefault(x =>
                    string.Compare(x.Name, subKey, StringComparison.OrdinalIgnoreCase) == 0);
                if (nesMi == null) return null;
                var obj = _accessor[_entity, entKey];
                return GetValue(acc, obj, nesMi.Type, subKey);
            }

            return GetValue(_accessor, _entity, mi.Type, propName);
        }

        private object GetRefEntityValue(Member mi, string propName)
        {
            var idKey = $"{mi.Name}Id";
            var idMemType = _members.FirstOrDefault(x =>
                string.Compare(x.Name, idKey, StringComparison.OrdinalIgnoreCase) == 0);
            if (idMemType == null) return null;

            if (mi.Type == typeof(OptionSet))
            {
                var obj = _accessor[_entity, idKey];
                return obj == null ? null : $"{obj}";
            }

            var rv = _accessor[_entity, idKey];
            if (rv == null) return null;

            var ls = "";
            var vp = _accessor[_entity, propName];
            if (vp == null) return null;
            
            var et = vp as IEntity;
            if (et == null) return null;

            ls = et.GetName();
            return JsonConvert.SerializeObject(new {value = rv, label = ls});
        }
    }
}