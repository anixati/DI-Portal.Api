using System.Collections.Generic;
using Di.Qry.Core;

namespace Di.Qry.Schema.Types
{
    public class Entity
    {
        private readonly string _primaryKey;

        public Entity(string name, string alias, string primaryKey = "", string schemaName = "dbo")
        {
            _primaryKey = string.IsNullOrEmpty(primaryKey) ? "Id" : primaryKey;
            Name = name;
            Alias = alias;
            Schema = schemaName;
        }

        public string Name { get; }
        public string TableName => $"[{Schema}].[{Name}] AS {Alias}";
        public string Alias { get; }
        public string Schema { get; }
        public string PrimaryKey => $"{Alias}.{_primaryKey}";
        public List<string> SortColumns { get; set; } = new List<string>();
        public List<GridColumn> Columns { get; set; } = new List<GridColumn>();
        public List<Field> Fields { get; set; } = new List<Field>();
        public Dictionary<string, Link> Links { get; set; } = new Dictionary<string, Link>();
        // public Dictionary<string, SubQryEntity> SubQueries { get; set; } = new Dictionary<string, SubQryEntity>();
        public List<string> Clauses { get; set; } = new List<string>();
        public static Entity Create(string name, string alias, string primaryKey = "", string schemaName = "dbo")
        {
            var entity = new Entity(name, alias, primaryKey, schemaName);
            entity.Columns.Add(new GridColumn("Id"));
            return entity;
        }
    }
}