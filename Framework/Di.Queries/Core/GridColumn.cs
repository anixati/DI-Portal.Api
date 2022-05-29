using System;
using Newtonsoft.Json;

namespace Di.Qry.Core
{
    public class GridColumn
    {
        public GridColumn()
        {
        }

        public GridColumn(string colName, string accessor = "", string header = "")
        {
            SortCol = colName;
            ColName = !string.IsNullOrEmpty(accessor) ? $"{colName} AS {accessor}" : $"{colName}";
            Accessor = !string.IsNullOrEmpty(accessor) ? $"{accessor}" : $"{SanitizeCol(colName)}";
            Header = !string.IsNullOrEmpty(header) ? $"{header}" : $"{Accessor.ToSentence()}";
        }

        [JsonIgnore] public string SortCol { get; }

        public bool Sortable { get; set; }

        [JsonIgnore] public bool Searchable { get; set; }

        [JsonIgnore] public string ColName { get; }

        public string Accessor { get; }

        [JsonProperty("Header")] public string Header { get; }

        public int Width { get; set; }
        public string Format { get; set; }
        public ColumnType Type { get; set; }
        public string TypeCode { get; set; }

        private string SanitizeCol(string colName)
        {
            return colName.Contains(".") ? colName[(colName.LastIndexOf('.') + 1)..] : colName;
        }
    }

    public enum ColumnType
    {
        Default = 0,
        Hidden,
        HyperLink,
        DateTime
    }
}