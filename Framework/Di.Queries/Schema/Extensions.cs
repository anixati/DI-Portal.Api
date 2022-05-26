using System;
using Di.Qry.Core;
using Di.Qry.Schema.Types;

namespace Di.Qry.Schema
{
    public static class Extensions
    {
        public static Table AddColExp(this Table table, string colExpression, string alias)
        {
            table.Columns.Add(new GridColumn(colExpression, alias));
            return table;
        }

        public static Table Select(this Table table, params string[] cols)
        {
            if (cols == null) return table;
            foreach (var colName in cols)
                if (colName.Contains("|"))
                {
                    var cs = colName.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                    table.Column(cs[0], cs[1]);
                }
                else
                {
                    table.Column(colName);
                }

            return table;
        }

        public static Table SelectSearchCols(this Table table, params string[] cols)
        {
            if (cols == null) return table;
            foreach (var colName in cols)
                if (colName.Contains("|"))
                {
                    var cs = colName.Split(new[] {"|"}, StringSplitOptions.RemoveEmptyEntries);
                    table.SearchCol(cs[0], cs[1]);
                }
                else
                {
                    table.SearchCol(colName);
                }

            return table;
        }

        public static Table SearchCol(this Table table, string colName, string accessor = "")
        {
            table.Column(colName, accessor, string.Empty, true, true);
            return table;
        }

        public static Table Column(this Table table, string colName, string accessor = "", string header = "",
            bool searchable = false, bool sortable = false)
        {
            table.Columns.Add(new GridColumn($"{table.Alias}.{colName}", accessor, header)
                {Searchable = searchable, Sortable = sortable});
            return table;
        }

        public static Table AddSortCol(this Table table, string colName, bool desc = false)
        {
            table.SortColumns.Add(new SortInfo($"{table.Alias}.{colName}", desc));
            return table;
        }

        public static Table AddQry(this Table table, string key, string name, string refDataId)
        {
            return table.AddQry(key, FieldType.OptionSet, name, x => x.ReferenceSchema = refDataId);
        }

        public static Table AddQry(this Table table, string key, FieldType fieldType, string name = "",
            Action<Field> configure = null)
        {
            var mf = new Field(table.Alias, key, fieldType, name);
            configure?.Invoke(mf);
            table.Fields.Add(mf);
            return table;
        }

        public static Table InnerJoin(this Table table, string name, string alias, string from, string to,
            Action<Table> configure = null)
        {
            var qe = new Table(name, alias);
            configure?.Invoke(qe);
            return table.Join(qe, from, to);
        }

        public static Table OuterJoin(this Table table, string name, string alias, string from, string to,
            Action<Table> configure = null)
        {
            var qe = new Table(name, alias);
            configure?.Invoke(qe);
            return table.Join(qe, from, to, LinkType.Outer);
        }

        public static Table Join(this Table table, Table lnkTable, string from, string to,
            LinkType linkType = LinkType.Default)
        {
            var key = $"{table.Name}_{lnkTable.Name}";
            table.Links[key] = new Link(key)
            {
                Table = lnkTable,
                From = $"{lnkTable.Alias}.{from}",
                To = $"{table.Alias}.{to}",
                LinkType = linkType
            };
            return table;
        }

        public static Table Join(this Table table, string name, string alias, string from)
        {
            var link = new Table(name, alias);
            var key = $"{table.Name}_{link.Name}";
            table.Links[key] = new Link(key)
            {
                Table = link,
                From = $"{link.Alias}.{from}",
                To = $"{table.PrimaryKey}",
                LinkType = LinkType.Default
            };
            return link;
        }
    }
}