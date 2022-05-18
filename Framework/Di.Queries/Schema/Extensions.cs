using System;
using Di.Qry.Core;
using Di.Qry.Schema.Types;

namespace Di.Qry.Schema
{
    public static class Extensions
    {
        public static Entity AddColExp(this Entity entity, string colExpression, string alias)
        {
            entity.Columns.Add(new GridColumn(colExpression, alias));
            return entity;
        }

        public static Entity AddCols(this Entity entity, params string[] cols)
        {
            if (cols == null) return entity;
            foreach (var colName in cols)
                if (colName.Contains("|"))
                {
                    var cs = colName.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                    entity.AddCol(cs[0], cs[1]);
                }
                else
                {
                    entity.AddCol(colName);
                }

            return entity;
        }

        public static Entity AddCol(this Entity entity, string colName, string accessor="",string header="" )
        {
            entity.Columns.Add(new GridColumn($"{entity.Alias}.[{colName}]", accessor, header));
            return entity;
        }

        public static Entity AddSortCol(this Entity entity, string colName)
        {
            entity.SortColumns.Add($"{entity.Alias}.{colName}");
            return entity;
        }

        public static Entity AddQry(this Entity entity, string key, string name, string refDataId)
        {
            return entity.AddQry(key, FieldType.OptionSet, name, x => x.ReferenceSchema = refDataId);
        }

        public static Entity AddQry(this Entity entity, string key, FieldType fieldType, string name = "",
            Action<Field> configure = null)
        {
            var mf = new Field(entity.Alias, key, fieldType, name);
            configure?.Invoke(mf);
            entity.Fields.Add(mf);
            return entity;
        }

        public static Entity InnerJoin(this Entity entity, string name, string alias, string from, string to,
            Action<Entity> configure = null)
        {
            var qe = new Entity(name, alias);
            configure?.Invoke(qe);
            return entity.Join(qe, from, to);
        }

        public static Entity OuterJoin(this Entity entity, string name, string alias, string from, string to,
            Action<Entity> configure = null)
        {
            var qe = new Entity(name, alias);
            configure?.Invoke(qe);
            return entity.Join(qe, from, to, LinkType.Outer);
        }

        public static Entity Join(this Entity entity, Entity lnkEntity, string from, string to,
            LinkType linkType = LinkType.Default)
        {
            var key = $"{entity.Name}_{lnkEntity.Name}";
            entity.Links[key] = new Link(key)
            {
                Entity = lnkEntity,
                From = $"{lnkEntity.Alias}.{from}",
                To = $"{entity.Alias}.{to}",
                LinkType = linkType
            };
            return entity;
        }
    }
}