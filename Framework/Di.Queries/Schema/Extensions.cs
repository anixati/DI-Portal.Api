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
                    var cs = colName.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                    table.Column(cs[0], cs[1]);
                }
                else
                {
                    table.Column(colName);
                }

            return table;
        }

        public static Table AddSearchCols(this Table table, params string[] cols)
        {
            if (cols == null) return table;
            foreach (var colName in cols)
              table.AddSearchColumn(colName);
            return table;
        }

        public static Table AddHiddenCols(this Table table, params string[] cols)
        {
            if (cols == null) return table;
            foreach (var colName in cols)
                table.Column(colName, colName, colName, x =>
                {
                    x.Type = ColumnType.Hidden;
                });

            return table;
        }
        public static Table AddSearchColumn(this Table table, string colName)
        {
            if (colName.Contains("|"))
            {
                var cs = colName.Split(new[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
                table.AddSearchCol(cs[0], cs[1]);
            }
            else
            {
                table.AddSearchCol(colName);
            }
            return table;
        }

        public static Table AddSearchCol(this Table table, string colName, string accessor = "")
        {
            table.Column(colName, accessor, string.Empty, true, true);
            return table;
        }

        public static Table AddDateColumn(this Table table, string colName, string accessor = "", string header = "",
            bool searchable = false, bool sortable = false)
        {
            table.Column(colName, accessor, header, x =>
            {
                x.Searchable = searchable;
                x.Sortable = sortable;
                x.Type = ColumnType.DateTime;
            });
            return table;
        }

        public static Table Column(this Table table, string colName, string accessor = "", string header = "",
            bool searchable = false, bool sortable = false)
        {
            table.Column(colName, accessor, header, x =>
            {
                x.Searchable = searchable;
                x.Sortable = sortable;
            });
            return table;
        }

        public static Table CalColumn(this Table table, string colExp, string accessor, string header,
            Action<GridColumn> configure = null)
        {
            var col = new GridColumn($"{colExp}", accessor, header)
            {
                SelectType = SelectType.Raw
            };
            configure?.Invoke(col);
            table.Columns.Add(col);
            return table;
        }

        public static Table Column(this Table table, string colName, string accessor, string header,
            Action<GridColumn> configure = null)
        {
            var col = new GridColumn($"{table.Alias}.{colName}", accessor, header);
            configure?.Invoke(col);
            table.Columns.Add(col);
            return table;
        }

        public static Table AddSortCol(this Table table, string colName, bool desc = false)
        {
            table.SortColumns.Add(new SortInfo($"{table.Alias}.{colName}", desc));
            return table;
        }

        public static Table AddQryField(this Table table, string key, string name, string refDataId)
        {
            return table.AddQryField(key, QryFieldType.OptionSet, name, x => x.ReferenceSchema = refDataId);
        }

        public static Table AddQryField(this Table table, string key, QryFieldType qryFieldType= QryFieldType.Text, string name = "",
            Action<QryField> configure = null)
        {
            var mf = new QryField(table.Alias, key, qryFieldType, name);
            configure?.Invoke(mf);
            table.QryFields.Add(mf);
            return table;
        }
        
        public static Table InnerJoin(this Table table, string name, string alias, string from, string to,
            Action<Table> configure = null, string schemaName = "dbo")
        {
            var qe = new Table(name, alias,"",schemaName);
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

        public static Table Join(this Table table, string name, string alias, string from, string schemaName = "dbo")
        {
            var link = new Table(name, alias,"", schemaName);
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