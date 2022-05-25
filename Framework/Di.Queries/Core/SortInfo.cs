namespace Di.Qry.Core
{
    public class SortInfo
    {
        public SortInfo(string name, bool desc = false)
        {
            Id = name;
            Desc = desc;
        }

        public string Id { get; }
        public bool Desc { get; }
    }
}