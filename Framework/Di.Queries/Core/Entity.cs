using System.Collections.Generic;

namespace Di.Qry.Core
{
    public class Entity
    {
        public Entity(string name, string alias = "", string schemaName = "dbo")
        {
            Name = name;
            Alias = string.IsNullOrEmpty(alias) ? name : alias;
            Schema = schemaName;
        }

        public string Name { get; }
        public string TableName => $"{Schema}.{Name} AS {Alias}";
        public string Alias { get; }
        public string Schema { get; }
        public List<string> SortColumns { get; set; } = new();
        public List<Col> Columns { get; set; } = new();
        public List<Field> Fields { get; set; } = new();
        public Dictionary<string, Link> Links { get; set; } = new();
    }
}