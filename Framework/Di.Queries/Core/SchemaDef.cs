using System.Collections.Generic;

namespace Di.Qry.Core
{
    public class SchemaDef
    {
        public string Title { get; set; }
        public List<GridColumn> Columns { get; set; } = new List<GridColumn>();
    }
}