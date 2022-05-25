using System.Collections.Generic;

namespace Di.Qry.Schema.Types
{
    public class Link
    {
        public Link(string name)
        {
            Name = name;
        }

        public string Name { get; }
        public Entity Entity { get; set; }
        public LinkType LinkType { get; set; } = LinkType.Default;
        public string From { get; set; }
        public string To { get; set; }

        public string JoinType => LinkType == LinkType.Default ? "inner join" : "left outer join";
        public List<string> Clauses { get; set; } = new();
    }
}