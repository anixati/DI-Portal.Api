using System.Collections.Generic;
using Di.Qry.Core;

namespace Di.Qry.Schema.Types
{
    public class Table
    {
        private readonly string _primaryKey;

        public Table(string name, string alias, string primaryKey = "", string schemaName = "dbo")
        {
            _primaryKey = string.IsNullOrEmpty(primaryKey) ? "Id" : primaryKey;
            Name = name;
            Alias = alias;
            Schema = schemaName;
        }

        public string Name { get; }
        public string TableName => $"{Schema}.{Name} AS {Alias}";
        public string Alias { get; }
        public string Schema { get; }
        public string PrimaryKey => $"{Alias}.{_primaryKey}";
        public List<SortInfo> SortColumns { get; set; } = new();
        public List<GridColumn> Columns { get; set; } = new();
        public List<Field> Fields { get; set; } = new();

        public Dictionary<string, Link> Links { get; set; } = new();

        // public Dictionary<string, SubQryEntity> SubQueries { get; set; } = new Dictionary<string, SubQryEntity>();
        public List<string> Clauses { get; set; } = new();

        public static Table Create(string name, string alias, string primaryKey = "", string schemaName = "dbo")
        {
            var entity = new Table(name, alias, primaryKey, schemaName);
            entity.Columns.Add(new GridColumn($"{entity.Alias}.Id", "Id", "Id")
                { Searchable = false, Sortable = false,Type = ColumnType.Hidden});
            return entity;
        }

        public static Table Create(TableKey key)
        {
            return Table.Create(key.Name,key.Alias);
        }
    }

    public class TableKey
    {
        public TableKey(string name, string @alias)
        {
            Name = name;
            Alias = alias;
        }

        public string Name { get; }
        public string Alias { get; }
    }
}