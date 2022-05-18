namespace Di.Qry.Core
{
    public class SortInfo
    {
        public SortInfo(string name, bool desc =false)
        {
            Name = name;
            Desc = desc;
        }

        public string Name { get;  }
        public bool Desc { get; }
    }
}